// -----------------------------------------------------------------------
// <copyright file="TestController.cs" company="Intermedia">
//   Copyright © Intermedia.net, Inc. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AccessTokenValidation.App.Controllers
{
    using System.Web.Http;

    public class TestController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("test/anonymous")]
        public IHttpActionResult Anonymous()
        {
            return this.Ok(new {Value = "anonymous"});
        }

        [Authorize]
        [HttpGet]
        [Route("test/authorize")]
        public IHttpActionResult Auth()
        {
            return this.Ok(new
            {
                User = this.User.Identity.Name,
            });
        }
    }
}