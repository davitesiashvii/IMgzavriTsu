using IMgzavri.Commands.Commands.Statment;
using IMgzavri.Queries.Queries.Statement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleSoft.Mediator;
using System.Net;

namespace IMgzavri.Api.Controllers
{
    [Route("api/statement")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StatementController : BaseController
    {
        public StatementController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("create-statment")]
        public async Task<IActionResult> CreateStatment([FromBody] CreateStatmentCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }

        [HttpPost("update-statment")]
        public async Task<IActionResult> UpdateStatement([FromBody] EditStatmrentCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }

        [HttpGet("get-statment/{id}")]
        public async Task<IActionResult> GetStatement(Guid id, CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new GetStatmentQuery(id), ct);

            return Ok(result);
        }

        [HttpPost("get-statments")]
        public async Task<IActionResult> GetCars(GetStatmentsQuery cmd, CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new GetStatmentsQuery(cmd.page,cmd.offset,cmd.StatmentFilter,cmd.SearchStatment,cmd.SortStatment), ct);

            return Ok(result);
        }
    }
}
