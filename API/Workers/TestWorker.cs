using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Workers
{
    public class TestWorker : IHostedService, IDisposable
    {
        private readonly ILogger<TestWorker> _logger;
        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private Task _executingTask;

        public TestWorker(ILogger<TestWorker> logger)
        {
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("my test background service is starting");
            _executingTask = Task.Run(() => Run(_cancelTokenSource.Token));
            return Task.CompletedTask;
        }

        private void Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var res = 2-2;
                    res = res/res;
                }
                catch (System.Exception)
                {

                    _logger.LogInformation("an error occured while running my test background service");
                }
                Thread.Sleep(1000);

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("my test background serice is stopping.");
            if (_executingTask == null)
            {
                return Task.CompletedTask;
            }
            _cancelTokenSource.Cancel();
            return Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));
        }

        public void Dispose()
        {
            _cancelTokenSource.Dispose();
        }
    }
}