using AutoMapper;
using FakeItEasy;
using FizzWare.NBuilder;
using Movie.Tests;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Feature.Administration.Queries;
using Movies.Feature.Queries;
using System;
using Xunit;

namespace Movies.Tests.Administration.Commands
{
    public class GetSearchByIdQueryHandlerTests : BaseTest
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IMapper _mapper;
        private readonly GetSearchByIdQueryHandler _getSearchByIdQueryHandler;

        public GetSearchByIdQueryHandlerTests()
        {
            _mongoDBService = A.Fake<IMongoDBService>();
            _mapper = CreateMapper();
            _getSearchByIdQueryHandler = new GetSearchByIdQueryHandler(_mongoDBService, _mapper);
        }

        [Fact]
        public async void GetSearchByIdQueryHandler_should_return_searches_by_id()
        {
            // Arrange
            var searches = Builder<Search>.CreateListOfSize(10)
                .TheFirst(1)
                   .With(s => s.Id = "1254")
                .TheRest()
                  .With(s => s.Id = Guid.NewGuid().ToString())
                .Build();
            A.CallTo(() => _mongoDBService.GetAsync("1254")).Returns(searches[0]);
            var command = new GetSearchByIdQuery
            {
                Id = "1254"
            };

            // Act
            var result = await _getSearchByIdQueryHandler.Handle(command, default);

            // Assert
            A.CallTo(() => _mongoDBService.GetAsync("1254")).MustHaveHappenedOnceExactly();
            Assert.Equal(searches[0].Id, result.Id);
        }
    }
}
