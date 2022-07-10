using IMgzavri.Commands.Commands.Profile;
using IMgzavri.Queries.Queries.Profile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleSoft.Mediator;
using System.Net;

namespace IMgzavri.Api.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController : BaseController
    {
        public ProfileController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("get-user-info")]
        public async Task<IActionResult> GetUserInfo(CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new GetUserInfoQuery(), ct);

            return Ok(result);
        }

        [HttpGet("get-rate/{userId}/{StatmentId}")]
        public async Task<IActionResult> GetRate(string userId,string StatmentId ,CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new GetRateQuery(userId,StatmentId), ct);

            return Ok(result);
        }

        [HttpPost("edit-user")]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }

        [HttpPost("create-rate")]
        public async Task<IActionResult> CreateRate([FromBody] CreateRateCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }


    }
}
