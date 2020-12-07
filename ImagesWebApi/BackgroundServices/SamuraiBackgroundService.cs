using System;
using System.Threading;
using System.Threading.Tasks;
using ImagesWebApi.Factory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ImagesWebApi.BackgroundServices 
{
    public class SamuraiBackgroundService : BackgroundService
    {
        private readonly BusinessLogicData _businessLogicData;
        private readonly ILogger<BusinessLogicData> _logger;

        public SamuraiBackgroundService(BusinessLogicData businessLogicData, ILogger<BusinessLogicData> logger)
        {
            _businessLogicData = businessLogicData;
            _logger = logger;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SamuraiBackgroundService is starting!");

            stoppingToken.Register(() => _logger.LogDebug("SamuraiBackgroundService is stopping!"));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("SamuraiBackgroundService is doing background work!");

                var samurai = SamuraiBogus.Create();
                _logger.LogInformation($"SamuraiBackgroundService is doing background work! Adding new samurai: {samurai.Name}");
                await _businessLogicData.CreateNewSamurai(samurai);

                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}