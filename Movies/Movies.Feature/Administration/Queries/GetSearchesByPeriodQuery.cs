using MediatR;
using Movies.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Feature.Administration.Queries
{
    public class GetSearchesByPeriodQuery : IRequest<IEnumerable<SearchDto>>
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
