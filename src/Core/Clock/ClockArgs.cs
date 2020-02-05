namespace Playground.Blazor.Core.Clock
{
    using System;

    public sealed class ClockArgs
    {
        public ClockArgs(DateTimeOffset raiseTime)
        {
            this.RaiseTime = raiseTime;
        }

        public DateTimeOffset RaiseTime { get; }
    }
}