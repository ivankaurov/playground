namespace Playground.Blazor.Core
{
    public interface ICountable
    {
        int InstanceCount { get; }

        int InstanceNumber { get; }
    }
}