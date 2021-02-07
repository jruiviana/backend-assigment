using AutoMapper;
using MediatR;
using Movies.Core.Interfaces;
using Movies.Core.Models;
using Movies.Feature.Administration.Queries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Feature.Queries
{
    public class GetSearchesByPeriodQueryHandler : IRequestHandler<GetSearchesByPeriodQuery, IEnumerable<SearchDto>>
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IMapper _mapper;

        public GetSearchesByPeriodQueryHandler(IMongoDBService mongoDBService, IMapper mapper)
        {
            _mongoDBService = mongoDBService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SearchDto>> Handle(GetSearchesByPeriodQuery request, CancellationToken cancellationToken)
        {
            if (request.EndDate <= request.StartDate)
            {
                throw new ValidationException("End date must be greater than start date.");
            }

            var result = await _mongoDBService.GetAsync(request.StartDate, request.EndDate);

            return _mapper.Map<List<SearchDto>>(result);
        }
    }
}
