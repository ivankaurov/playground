namespace Playground.Blazor.Core.Clock
{
    using System;

    public sealed class ClockServiceConfiguration
    {
        public TimeSpan RaiseInterval { get; set; } = new TimeSpan(0, 0, 1);
    }
}