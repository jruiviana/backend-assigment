using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Movies.Feature.Administration.Commands
{
    public class DeleteSearchCommand : IRequest<long>
    {
        [Required]
        public string Id { get; set; }
    }
}
