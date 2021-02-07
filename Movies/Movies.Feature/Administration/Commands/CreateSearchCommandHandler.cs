using MediatR;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Feature.Administration.Commands
{
    public class CreateSearchCommandHandler : IRequestHandler<CreateSearchCommand, string>
    {
        private readonly IMongoDBService _mongoDBService;

        public CreateSearchCommandHandler(IMongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        public async Task<string> Handle(CreateSearchCommand request, CancellationToken cancellationToken)
        {
            var search = new Search()
            {
                ImdbID = request.ImdbID,
                Ip_Address = request.Ip_Address,
                Processing_Time_Ms = request.Processing_Time_Ms,
                Search_Token = request.Search_Token,
                Timestamp = DateTime.Now
            };

            var result = await _mongoDBService.CreateAsync(search);

            return result.Id;
        }
    }
}
