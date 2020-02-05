namespace Playground.Blazor.Core.Events
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Playground.Blazor.Core.Utils;

    internal sealed class EventBus : IEventBus
    {
        private readonly ConcurrentDictionary<Type, IDictionary<Guid, Delegate>> handlers =
            new ConcurrentDictionary<Type, IDictionary<Guid, Delegate>>();

        public IDisposable Handle<TEvent>(Func<TEvent, Task> handler)
        {
            var handlerId = Guid.NewGuid();
            this.handlers.AddOrUpdate(
                typeof(TEvent),
                _ => new ConcurrentDictionary<Guid, Delegate> { [handlerId] = handler },
                (_, v) =>
                    {
                        v.Add(handlerId, handler);
                        return v;
                    });
            return new FunctionalDisposable(() => this.handlers[typeof(TEvent)].Remove(handlerId));
        }

        public void Raise<TEvent>(TEvent eventArgs)
        {
            this.RaiseInternal(eventArgs).Forget();
        }

        private async Task RaiseInternal<TEvent>(TEvent args)
        {
            await Task.Yield();
            if (!this.handlers.TryGetValue(typeof(TEvent), out var eventHandlers))
            {
                return;
            }

            await Task.WhenAll(eventHandlers.Values.Select(handler => this.CallHandler(handler, args)))
                .ConfigureAwait(false);
        }

        private async Task CallHandler<TEvent>(Delegate handler, TEvent args)
        {
            try
            {
                await ((Func<TEvent, Task>)handler)(args).ConfigureAwait(false);
            }
            catch
            {
                // TODO: log error;
            }
        }
    }
}