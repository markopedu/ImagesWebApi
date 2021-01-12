using System.Collections.Generic;
using System.IO;
using ImagesWebApi.BackgroundServices;
using ImagesWebApi.Models.Dto;
using ImagesWebApi.Services;
using ImagesWebApi.Services.Cache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamuraiApp.Data;
using Serilog;
using StackExchange.Redis;

namespace ImagesWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            HostingEnvironment = environment;
        }

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => { builder.WithOrigins("*"); });
            });

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration: Configuration.GetSection("Logging"))
                    .AddSerilog(new LoggerConfiguration().WriteTo.File("serilog.txt").CreateLogger())
                    .AddConsole();
            });

            services.AddControllers();

            services.AddDirectoryBrowser();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<ICarouselImageService, CarouselImageService>();

            services.AddDbContext<SamuraiContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("SamuraiConnection"))
                    .EnableSensitiveDataLogging();
            });
            services.AddScoped<BusinessLogicData>();
            
            services.AddHostedService<SamuraiBackgroundService>();

            var redisConn = Configuration.GetValue<string>("RedisConnection");
            services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(redisConn));
            
            services.AddSingleton<ICacheService<string>, RedisCacheService>();
            services.AddSingleton<ICacheService<SamuraiDto>, SamuraiCacheService>();
            services.AddSingleton<ICacheService<IEnumerable<SamuraiDto>>, SamuraiCacheListService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            const string cacheMaxAge = "604800";
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "MyStaticFiles")),
                RequestPath = "/StaticFiles",
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cacheMaxAge}");
                }
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}