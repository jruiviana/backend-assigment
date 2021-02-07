using MediatR;
using Movies.Core.Models;
using System.Collections.Generic;

namespace Movies.Feature.Administration.Queries
{
    public class GetAllSearchesQuery : IRequest<IEnumerable<SearchDto>>
    { }
}
