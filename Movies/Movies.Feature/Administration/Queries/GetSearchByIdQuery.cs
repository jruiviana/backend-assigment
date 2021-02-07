using MediatR;
using Movies.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Feature.Administration.Queries
{
    public class GetSearchByIdQuery : IRequest<SearchDto>
    {
        [Required]
        public string Id { get; set; }
    }
}
