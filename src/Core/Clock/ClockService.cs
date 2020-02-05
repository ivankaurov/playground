namespace Playground.Blazor.Core.Clock
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;

    using Playground.Blazor.Core.Events;

    internal sealed class ClockService : BackgroundService
    {
        private readonly IEventBus eventBus;

        private readonly ClockServiceConfiguration configuration;

        public ClockService(IEventBus eventBus, IOptions<ClockServiceConfiguration> configuration)
        {
            this.eventBus = eventBus;
            this.configuration = configuration.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                this.eventBus.Raise(new ClockArgs(DateTimeOffset.Now));
                try
                {
                    await Task.Delay(this.configuration.RaiseInterval, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
    }
}