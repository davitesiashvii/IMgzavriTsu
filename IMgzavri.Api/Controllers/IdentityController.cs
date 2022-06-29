using IMgzavri.Commands.Commands.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleSoft.Mediator;

namespace IMgzavri.Api.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : BaseController
    {
        public IdentityController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand cmd, CancellationToken ct)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(new AuthFailedModel
            //    {
            //        Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
            //    });
            //}

            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }

        [HttpPost("vertify-email-and-send-validate-code")]
        public async Task<IActionResult> VertifyEmail([FromBody] VertifyEmailAndSendValidateCodeCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }

        [HttpPost("validate-code")]
        public async Task<IActionResult> ValidateCode([FromBody] ValidateCodeCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }


        [HttpPost("restore-password")]
        public async Task<IActionResult> RestorePassword([FromBody] RestorePasswordCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test(CancellationToken ct)
        {
            return Ok("respect");
        }
    }
}
