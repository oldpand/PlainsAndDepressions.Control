using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlainsAndDepressions.Control.Requests;
using PlainsAndDepressions.Control.Services.Commands;

namespace PlainsAndDepressions.Control.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ControlController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ControlController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> PutPad([FromBody]PutDepressionsRequest request)
        {
            var result = await _mediator.Send(new PutPuckCommand(request.PackId, request.Pack));

            if (!result.IsSuccess)
            {
                BadRequest(result);
            }

            return Ok(result);
        }
    }
}
