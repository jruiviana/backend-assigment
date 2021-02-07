using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Feature.Administration.Commands;
using Movies.Feature.Administration.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = "API Key")]
    public class AdministrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MoviesController> _logger;

        public AdministrationController(ILogger<MoviesController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("searches")]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Administrator get all searches");
            var result = await _mediator.Send(new GetAllSearchesQuery(), cancellationToken);

            if (result is null)
            {
                return NotFound("Search not found!");
            }

            return Ok(result);
        }

        [HttpGet("searches/{Id}")]
        public async Task<IActionResult> GetAsync([FromRoute] GetSearchByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Administrator get {query.Id} search");
            var result = await _mediator.Send(query, cancellationToken);

            if (result is null)
            {
                return NotFound("Search not found!");
            }

            return Ok(result);
        }

        [HttpGet("searches/period")]
        public async Task<IActionResult> GetAsync([FromQuery] GetSearchesByPeriodQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Administrator get searches by period {query.StartDate} and {query.EndDate} ");
            var result = await _mediator.Send(query, cancellationToken);

            if (result is null)
            {
                return NotFound("Search not found!");
            }

            return Ok(result);
        }

        [HttpGet("searches/report")]
        public async Task<IActionResult> GetAsync([FromQuery] GetSearchReportQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Administrator get search report by period {query.StartDate} and {query.EndDate} ");
            var result = await _mediator.Send(query, cancellationToken);

            if (result is null)
            {
                return NotFound("Report not found!");
            }

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> GetAsync([FromRoute] DeleteSearchCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Administrator delete search {command.Id } ");
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
