namespace Playground.Blazor.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    public abstract class CountableBase : ICountable
    {
        private static readonly ConcurrentDictionary<Type, int> InstanceCounter = new ConcurrentDictionary<Type, int>();

        protected CountableBase()
        {
            this.InstanceNumber = InstanceCounter.AddOrUpdate(this.GetType(), _ => 1, (t, v) => v + 1);
        }

        public int InstanceCount => InstanceCounter[this.GetType()];

        public int InstanceNumber { get; }
    }
}