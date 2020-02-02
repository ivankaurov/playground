namespace Playground.Blazor.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("calc")]
    public class CalculatorController : ControllerBase
    {
        [HttpGet]
        [Route("{a:double}/multiply/{b:double}")]
        public ActionResult<double> Multiply([FromRoute] double a, [FromRoute] double b)
        {
            return a * b;
        }
    }
}