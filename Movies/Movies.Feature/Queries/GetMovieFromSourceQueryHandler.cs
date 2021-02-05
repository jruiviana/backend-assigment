using MediatR;
using Movies.Core.Interfaces;
using Movies.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Feature.Queries
{
    public class GetMovieFromSourceQueryHandler : IRequestHandler<GetMovieFromSourceQuery, SourceMovieDto>
    {
        private readonly ISourceMovieService _sourceMovieService;

        public GetMovieFromSourceQueryHandler(ISourceMovieService sourceMovieService)
        {
            _sourceMovieService = sourceMovieService;
        }

        public async Task<SourceMovieDto> Handle(GetMovieFromSourceQuery request, CancellationToken cancellationToken)
        {
            return await _sourceMovieService.GetMovieAsync(request.MovieTitle);
        }
    }
}
