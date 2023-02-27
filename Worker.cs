using System.Diagnostics;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace toolboxmamaneg
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                WebSocketServer wss = new WebSocketServer("ws://localhost:9292");
                try
                {
                    wss.Start();

                    wss.AddWebSocketService<SessionId>("/getport");
                    _logger.LogInformation("ToolboxManager started on port 9292");
                }
                catch (Exception ex)
                {
                    
                    _logger.LogInformation("ToolboxManager Exception {ex}", ex);
                }
                

         
              
                
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
        }
    }
}