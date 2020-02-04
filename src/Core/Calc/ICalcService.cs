namespace Playground.Blazor.Core.Calc
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICalcService
    {
        Task<double> Multiply(double a, double b, CancellationToken cancellationToken = default);
    }
}