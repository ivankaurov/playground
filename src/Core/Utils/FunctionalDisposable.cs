namespace Playground.Blazor.Core.Utils
{
    using System;

    internal sealed class FunctionalDisposable : IDisposable
    {
        private readonly Action onDispose;

        private bool objectDisposed;

        public FunctionalDisposable(Action onDispose)
        {
            this.onDispose = onDispose;
        }

        public void Dispose()
        {
            if (!this.objectDisposed)
            {
                this.onDispose();
                this.objectDisposed = true;
            }
        }
    }
}