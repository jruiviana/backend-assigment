using AutoMapper;
using MediatR;
using Movies.Core.Interfaces;
using Movies.Core.Models;
using Movies.Feature.Administration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Feature.Queries
{
    public class GetAllSearchesQueryHandler : IRequestHandler<GetAllSearchesQuery, IEnumerable<SearchDto>>
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IMapper _mapper;

        public GetAllSearchesQueryHandler(IMongoDBService mongoDBService, IMapper mapper)
        {
            _mongoDBService = mongoDBService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SearchDto>> Handle(GetAllSearchesQuery request, CancellationToken cancellationToken)
        {
            var result = await _mongoDBService.GetAsync();

            return _mapper.Map<List<SearchDto>>(result);
        }
    }
}
