using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Taller.Api.Commands;
using Taller.Api.Entities;
using Taller.Api.Repositories;
using Taller.Core.Controllers;
using Taller.Core.Extensions;

namespace Taller.Api.Controllers
{
    [Route("api/cars")]
    [Authorize]
    public class CarsController : MainController
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet("all")]
        public async Task<PagedResult<Car>> Get([FromQuery] int ps = 8,
                                                      [FromQuery] int page = 1,
                                                      [FromQuery] string q = null)
        {
            return await _carRepository.GetAllCarsPaged(ps, page, q);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromServices] IMediator _mediator, AddCarCommand command)
        {
            
            return CustomResponse(await _mediator.Send(command));
        }
    }

}
