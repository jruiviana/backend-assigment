using AutoMapper;
using FakeItEasy;
using FizzWare.NBuilder;
using Movie.Tests;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Core.Models;
using Movies.Feature.Administration.Queries;
using Movies.Feature.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Movies.Tests.Administration.Commands
{
    public class GetSearchReportQueryHandlerTests : BaseTest
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IMapper _mapper;
        private readonly GetSearchReportQueryHandler _getSearchReportQueryHandler;

        public GetSearchReportQueryHandlerTests()
        {
            _mongoDBService = A.Fake<IMongoDBService>();
            _mapper = CreateMapper();
            _getSearchReportQueryHandler = new GetSearchReportQueryHandler(_mongoDBService, _mapper);
        }

        [Fact]
        public async void GetSearchReportQueryHandler_should_return_search_report_by_period()
        {
            // Arrange
            var searches = Builder<SearchReport>.CreateListOfSize(10)
                .TheFirst(1)
                   .With(s => s.TotalRequests = 10)
                   .With(s => s.Date = new DateTime(2021, 2, 3))
                .TheRest()
                  .With(s => s.TotalRequests = 3)
                  .With(s => s.Date = new DateTime(2021, 3, 3))
                .Build();
            A.CallTo(() => _mongoDBService.GetSearchReportAsync(new DateTime(2021, 2, 1), new DateTime(2021, 2, 4))).Returns(new List<SearchReport>() { searches[0] });
            var command = new GetSearchReportQuery
            {
                StartDate = new DateTime(2021, 2, 1),
                EndDate = new DateTime(2021, 2, 4)
            };

            // Act
            var result = await _getSearchReportQueryHandler.Handle(command, default);

            // Assert
            A.CallTo(() => _mongoDBService.GetSearchReportAsync(new DateTime(2021, 2, 1), new DateTime(2021, 2, 4))).MustHaveHappenedOnceExactly();
            Assert.Equal(searches[0].TotalRequests, result.ToList()[0].TotalRequests);
        }

        [Fact]
        public async void CreateSearchCommand_should_return_ValidationException_when_period_end_greater_that_period_start()
        {
            // Arrange
            var command = new GetSearchReportQuery
            {
                StartDate = new DateTime(2021, 2, 4),
                EndDate = new DateTime(2021, 2, 1)
            };

            // Act Assert
            await Assert.ThrowsAsync<ValidationException>(async () =>
            await _getSearchReportQueryHandler.Handle(command, default));
        }
    }
}
