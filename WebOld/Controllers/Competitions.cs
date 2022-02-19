using Application.Competitions.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebOld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Competitions : ControllerBase
    {
        private readonly IMediator _mediator;

        public Competitions(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost("start/{id:guid}")]
        public async Task<IActionResult> Start(Guid id)
        {
            var command = new StartCompetition(id);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("choose-winner/{id:guid}")]
        public async Task<IActionResult> ChooseWinner(Guid id)
        {
            var command = new ChooseWinner(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
