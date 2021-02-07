using MediatR;
using Movies.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Feature.Queries
{
    public class GetMovieFromSourceQuery : IRequest<SourceMovieDto>
    {
        [Required(ErrorMessage = "Movie title is required!")]
        public string Title { get; set; }
    }
}
