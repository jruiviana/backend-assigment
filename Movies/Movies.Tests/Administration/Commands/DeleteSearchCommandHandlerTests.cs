using FakeItEasy;
using Movie.Tests;
using Movies.Core.Entities;
using Movies.Core.Interfaces;
using Movies.Feature.Administration.Commands;
using Xunit;

namespace Movies.Tests.Administration.Commands
{
    public class DeleteSearchCommandHandlerTests : BaseTest
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly DeleteSearchCommandHandler _deleteSearchCommandHandler;

        public DeleteSearchCommandHandlerTests()
        {
            _mongoDBService = A.Fake<IMongoDBService>();
            _deleteSearchCommandHandler = new DeleteSearchCommandHandler(_mongoDBService);
        }

        [Fact]
        public async void DeleteSearchCommandHandler_should_delete_search()
        {
            // Arrange
            A.CallTo(() => _mongoDBService.RemoveAsync(A<string>.Ignored)).Returns(1);
            var command = new DeleteSearchCommand
            {
                Id = "1254785369874"
            };

            // Act
            var result = await _deleteSearchCommandHandler.Handle(command, default);

            // Assert
            A.CallTo(() => _mongoDBService.RemoveAsync(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(1, result);
        }
    }
}
