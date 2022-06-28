using ExperianTest.RabbitMqConfiguration;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ExperianTest.Backgroundservices
{
    public class myservices : BackgroundService
    {
        private readonly ILogger<myservices> _logger;
        private readonly IServer server;
        private readonly IConsumerService _consumerService;
        public myservices(ILogger<myservices> _environment,
            IServer server,
            IConsumerService consumerService)
        {
            this._logger = _environment;
            this.server = server;
            this._consumerService = consumerService;
        }
        public myservices() { }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"GracePeriod task doing background work.");
                
                // This eShopOnContainers method is querying a database table
                // and publishing events into the Event Bus (RabbitMQ / ServiceBus)
                
                // GetCompanySummary();
                await _consumerService.ReadMessgaes();
                await Task.Delay(new TimeSpan(0, 0, 20), stoppingToken);
                _logger.LogInformation($"GracePeriod background task is stopping.");

            }
            

        }

      
    }
}
