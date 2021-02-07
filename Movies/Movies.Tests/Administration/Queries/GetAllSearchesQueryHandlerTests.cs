using AutoMapper;
using FakeItEasy;
using FizzWare.NBuilder;
using Movie.Tests;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Feature.Administration.Queries;
using Movies.Feature.Queries;
using System;
using System.Linq;
using Xunit;

namespace Movies.Tests.Administration.Commands
{
    public class GetAllSearchesQueryHandlerTests : BaseTest
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IMapper _mapper;
        private readonly GetAllSearchesQueryHandler _getAllSearchesQueryHandler;

        public GetAllSearchesQueryHandlerTests()
        {
            _mongoDBService = A.Fake<IMongoDBService>();
            _mapper = CreateMapper();
            _getAllSearchesQueryHandler = new GetAllSearchesQueryHandler(_mongoDBService, _mapper);
        }

        [Fact]
        public async void GetAllSearchesQueryHandler_should_return_searches()
        {
            // Arrange
            var searches = Builder<Search>.CreateListOfSize(10)
                .All()
                  .With(s => s.Id = Guid.NewGuid().ToString())
                .Build();
            A.CallTo(() => _mongoDBService.GetAsync()).Returns(searches.ToList());
            var command = new GetAllSearchesQuery();

            // Act
            var result = await _getAllSearchesQueryHandler.Handle(command, default);

            // Assert
            A.CallTo(() => _mongoDBService.GetAsync()).MustHaveHappenedOnceExactly();
            Assert.Equal(10, result.ToList().Count);
        }
    }
}
