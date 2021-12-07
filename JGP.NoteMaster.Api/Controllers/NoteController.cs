namespace JGP.NoteMaster.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Class NoteController.
    ///     Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiVersion("1")]
    [ApiController]
    [Route("v{version:apiVersion}/sms")]
    //[Authorize]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class NoteController : ControllerBase
    {
        /// <summary>
        ///     Helloes this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<string> Hello()
        {
            await Task.Delay(1000);
            return "Hello World!";
        }
    }
}