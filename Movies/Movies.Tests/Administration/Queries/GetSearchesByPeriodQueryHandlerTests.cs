using AutoMapper;
using FakeItEasy;
using FizzWare.NBuilder;
using Movie.Tests;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Feature.Administration.Queries;
using Movies.Feature.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Movies.Tests.Administration.Commands
{
    public class GetSearchesByPeriodQueryHandlerTests : BaseTest
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IMapper _mapper;
        private readonly GetSearchesByPeriodQueryHandler _getSearchesByPeriodQueryHandler;

        public GetSearchesByPeriodQueryHandlerTests()
        {
            _mongoDBService = A.Fake<IMongoDBService>();
            _mapper = CreateMapper();
            _getSearchesByPeriodQueryHandler = new GetSearchesByPeriodQueryHandler(_mongoDBService, _mapper);
        }

        [Fact]
        public async void GetSearchesByPeriodQueryHandler_should_return_searches_by_period()
        {
            // Arrange
            var searches = Builder<Search>.CreateListOfSize(10)
                .TheFirst(1)
                   .With(s => s.Id = "1254")
                   .With(s => s.Timestamp = new DateTime(2021, 2, 3))
                .TheRest()
                  .With(s => s.Id = Guid.NewGuid().ToString())
                  .With(s => s.Timestamp = new DateTime(2021, 3, 3))
                .Build();
            A.CallTo(() => _mongoDBService.GetAsync(new DateTime(2021, 2, 1), new DateTime(2021, 2, 4))).Returns(new List<Search>() { searches[0] });
            var command = new GetSearchesByPeriodQuery
            {
                StartDate = new DateTime(2021, 2, 1),
                EndDate = new DateTime(2021, 2, 4)
            };

            // Act
            var result = await _getSearchesByPeriodQueryHandler.Handle(command, default);

            // Assert
            A.CallTo(() => _mongoDBService.GetAsync(new DateTime(2021, 2, 1), new DateTime(2021, 2, 4))).MustHaveHappenedOnceExactly();
            Assert.Equal(searches[0].Id, result.ToList()[0].Id);
        }

        [Fact]
        public async void CreateSearchCommand_should_return_ValidationException_when_period_end_greater_that_period_start()
        {
            // Arrange
            var command = new GetSearchesByPeriodQuery
            {
                StartDate = new DateTime(2021, 2, 4),
                EndDate = new DateTime(2021, 2, 1)
            };

            // Act Assert
            await Assert.ThrowsAsync<ValidationException>(async () =>
            await _getSearchesByPeriodQueryHandler.Handle(command, default));
        }
    }
}
