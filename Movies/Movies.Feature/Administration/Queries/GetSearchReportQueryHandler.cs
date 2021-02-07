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
    public class GetSearchReportQueryHandler : IRequestHandler<GetSearchReportQuery, IEnumerable<SearchReportDto>>
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IMapper _mapper;

        public GetSearchReportQueryHandler(IMongoDBService mongoDBService, IMapper mapper)
        {
            _mongoDBService = mongoDBService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SearchReportDto>> Handle(GetSearchReportQuery request, CancellationToken cancellationToken)
        {
            if (request.EndDate <= request.StartDate)
            {
                throw new ValidationException("End date must be greater than start date.");
            }

            var result = await _mongoDBService.GetSearchReportAsync(request.StartDate, request.EndDate);

            return _mapper.Map<List<SearchReportDto>>(result);
        }
    }
}
