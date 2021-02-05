using MediatR;
using Movies.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Feature.Queries
{
    public class GetMovieFromSourceQuery : IRequest<SourceMovieDto>
    {
        public GetMovieFromSourceQuery(string movieTitle)
        {
            MovieTitle = movieTitle;
        }

        [Required]
        public string MovieTitle { get; }
    }
}
