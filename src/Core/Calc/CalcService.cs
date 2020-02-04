namespace Playground.Blazor.Core.Calc
{
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class CalcService : ICalcService
    {
        public Task<double> Multiply(double a, double b, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(a * b);
        }
    }
}