namespace Playground.Blazor.Api.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Playground.Blazor.Core.Calc;

    [ApiController]
    [Route("calc")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalcService calcService;

        public CalculatorController(ICalcService calcService)
        {
            this.calcService = calcService;
        }

        [HttpGet]
        [Route("{a:double}/multiply/{b:double}")]
        public async Task<ActionResult<double>> Multiply([FromRoute] double a, [FromRoute] double b, CancellationToken cancellationToken)
        {
            return await this.calcService.Multiply(a, b, cancellationToken);
        }
    }
}