using MediatR;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Feature.Administration.Commands
{
    public class DeleteSearchCommandHandler : IRequestHandler<DeleteSearchCommand, long>
    {
        private readonly IMongoDBService _mongoDBService;

        public DeleteSearchCommandHandler(IMongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        public async Task<long> Handle(DeleteSearchCommand request, CancellationToken cancellationToken)
        {
            return await _mongoDBService.RemoveAsync(request.Id);
        }
    }
}
