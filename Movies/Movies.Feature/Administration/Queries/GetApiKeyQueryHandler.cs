using AutoMapper;
using MediatR;
using Movies.Core.Interfaces;
using Movies.Core.Models;
using Movies.Feature.Administration.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Feature.Queries
{
    public class GetApiKeyQueryHandler : IRequestHandler<GetApiKeyQuery, ApiKeyDto>
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IMapper _mapper;

        public GetApiKeyQueryHandler(IMongoDBService mongoDBService, IMapper mapper)
        {
            _mongoDBService = mongoDBService;
            _mapper = mapper;
        }

        public async Task<ApiKeyDto> Handle(GetApiKeyQuery request, CancellationToken cancellationToken)
        {
            var result = await _mongoDBService.GetApiKeyAsync(request.Key);

            return _mapper.Map<ApiKeyDto>(result);
        }
    }
}
