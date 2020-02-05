namespace Playground.Blazor.Core.Events
{
    using System;
    using System.Threading.Tasks;

    public interface IEventBus
    {
        IDisposable Handle<TEvent>(Func<TEvent, Task> handler);

        void Raise<TEvent>(TEvent eventArgs);
    }
}