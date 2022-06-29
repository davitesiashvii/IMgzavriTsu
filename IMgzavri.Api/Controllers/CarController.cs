using IMgzavri.Commands.Commands.car;
using IMgzavri.Queries.Queries.Car;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleSoft.Mediator;

namespace IMgzavri.Api.Controllers
{
    [Route("api/car")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarController : BaseController
    {
        public CarController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("get-car-marcks")]
        public async Task<IActionResult> GetCarMarcks(CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new GetCarMarckQuery(), ct);

            return Ok(result);
        }

        [HttpGet("get-car-models/{marckId}")]
        public async Task<IActionResult> GetCarMarcks(int marckId, CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new GetCarModelsQuery(marckId), ct);

            return Ok(result);
        }




        [HttpGet("get-cars")]
        public async Task<IActionResult> GetCars(Guid userId, CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new GetCarsQuery(), ct);

            return Ok(result);
        }

        [HttpGet("get-car/{carId}")]
        public async Task<IActionResult> GetCar(Guid carId, CancellationToken ct)
        {
            var result = await Mediator.FetchAsync(new GetCarQuery(carId), ct);

            return Ok(result);
        }


        [HttpPost("create-car")]
        public async Task<IActionResult> CreateCar([FromBody] CreateCarCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }

        [HttpPost("update-car")]
        public async Task<IActionResult> UpdateCar([FromBody] UpdateCarCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }

        [HttpPost("delete-car")]
        public async Task<IActionResult> UpdateCar([FromBody] DeleteCarCommand cmd, CancellationToken ct)
        {
            var result = await Mediator.SendAsync(cmd, ct);

            return Ok(result);
        }

    }
}
