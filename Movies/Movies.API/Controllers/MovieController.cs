using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Feature.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string title, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    return BadRequest();
                }

                var query = new GetMovieFromSourceQuery(title);
                _logger.LogInformation($"Search for {title}");
                var result = await _mediator.Send(query, cancellationToken);

                if (result is null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
    }
}
