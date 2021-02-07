using FakeItEasy;
using FizzWare.NBuilder;
using Movie.Tests;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Feature.Administration.Commands;
using System;
using Xunit;

namespace Movies.Tests.Administration.Commands
{
    public class CreateSearchCommandHandlerTests : BaseTest
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly CreateSearchCommandHandler _createSearchCommandHandler;

        public CreateSearchCommandHandlerTests()
        {
            _mongoDBService = A.Fake<IMongoDBService>();
            _createSearchCommandHandler = new CreateSearchCommandHandler(_mongoDBService);
        }

        [Fact]
        public async void CreateSearchCommandHandler_should_create_search()
        {
            // Arrange
            var search = Builder<Search>.CreateNew()
               .With(s => s.Id = "601fd748f74809d35385e178")
               .With(s => s.ImdbID = "tt0076759")
               .With(s => s.Ip_Address = "192.168.0.1")
               .With(s => s.Processing_Time_Ms = 351)
               .With(s => s.Search_Token = "a")
               .With(s => s.Timestamp = new System.DateTime(2021, 1, 1))
           .Build();
            A.CallTo(() => _mongoDBService.CreateAsync(A<Search>.Ignored)).Returns(search);
            var command = new CreateSearchCommand("a", "tt0076759", 351, "192.168.0.1");

            // Act
            var result = await _createSearchCommandHandler.Handle(command, default);

            // Assert
            A.CallTo(() => _mongoDBService.CreateAsync(A<Search>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
        }

        [Fact]
        public async void CreateSearchCommand_should_return_ArgumentNullException_required_paramenter_null()
        {
            // Act Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _createSearchCommandHandler.Handle(new CreateSearchCommand(null, "tt0076759", 351, "192.168.0.1"), default));
        }
    }
}
