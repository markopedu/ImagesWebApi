using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ImagesWebApi.Factory;
using ImagesWebApi.Models.Dto;
using ImagesWebApi.Services.Cache;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ImagesWebApi.BackgroundServices 
{
    public class SamuraiBackgroundService : BackgroundService
    {
        private readonly ILogger<BusinessLogicData> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        // 1 minute.
        private const int Delay = 60000;

        public SamuraiBackgroundService(ILogger<BusinessLogicData> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SamuraiBackgroundService is starting!");

            stoppingToken.Register(() => _logger.LogDebug("SamuraiBackgroundService is stopping!"));

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var businessLogicData = scope.ServiceProvider.GetService<BusinessLogicData>();

                    if (businessLogicData == null)
                    {
                        _logger.LogInformation("Could not grab BusinessLogicData service inside SamuraiBackgroundService.");
                        return;
                    }
                     
                    _logger.LogInformation("SamuraiBackgroundService is doing background work!");

                    var samurai = SamuraiBogus.Create();
                    _logger.LogInformation($"SamuraiBackgroundService is doing background work! Adding new samurai: {samurai.Name}");
                    await businessLogicData.CreateNewSamurai(samurai);

                    var samuraiRedisCacheList = scope.ServiceProvider.GetService<ICacheService<IEnumerable<SamuraiDto>>>();
                    if (samuraiRedisCacheList != null)
                    {
                        await samuraiRedisCacheList.RemoveKey(CacheKeys.CacheKeySamuraiList);
                        _logger.LogInformation("Clean Up Redis Cache: CacheKeySamuraiList");
                    } 
                    
                }

                await Task.Delay(Delay, stoppingToken);
            }
        }
    }
}