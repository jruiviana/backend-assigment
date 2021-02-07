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
    public class GetSearchByIdQueryHandler : IRequestHandler<GetSearchByIdQuery, SearchDto>
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IMapper _mapper;

        public GetSearchByIdQueryHandler(IMongoDBService mongoDBService, IMapper mapper)
        {
            _mongoDBService = mongoDBService;
            _mapper = mapper;
        }

        public async Task<SearchDto> Handle(GetSearchByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mongoDBService.GetAsync(request.Id);

            return _mapper.Map<SearchDto>(result);
        }
    }
}
