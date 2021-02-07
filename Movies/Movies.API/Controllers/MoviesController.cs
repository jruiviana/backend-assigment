using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Feature.Administration.Commands;
using Movies.Feature.Queries;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(ILogger<MoviesController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetMovieFromSourceQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Search for {query.Title}");
            var sw = Stopwatch.StartNew();
            var result = await _mediator.Send(query, cancellationToken);
            sw.Stop();

            // Create search entry in the database
            await _mediator.Send(
                new CreateSearchCommand(
                    query.Title,
                    result is null ? string.Empty : result.ImdbID,
                    sw.ElapsedMilliseconds,
                    HttpContext.Connection.RemoteIpAddress.ToString()), cancellationToken);

            if (result is null)
            {
                return NotFound("Movie not found!");
            }

            return Ok(result);
        }
    }
}
