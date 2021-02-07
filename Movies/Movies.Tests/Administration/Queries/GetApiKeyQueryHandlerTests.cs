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
    public class GetApiKeyQueryHandlerTests : BaseTest
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly IMapper _mapper;
        private readonly GetApiKeyQueryHandler _getApiKeyQueryHandler;

        public GetApiKeyQueryHandlerTests()
        {
            _mongoDBService = A.Fake<IMongoDBService>();
            _mapper = CreateMapper();
            _getApiKeyQueryHandler = new GetApiKeyQueryHandler(_mongoDBService, _mapper);
        }

        [Fact]
        public async void GetApiKeyQueryHandler_should_return_apikey_by_key()
        {
            // Arrange
            var apiKey = Builder<ApiKey>.CreateNew()
                .With(s => s.Key = "68869c32-d971-40f0-8b12-7392a153cd94")
                .With(s => s.Owner = "Administration")
                .With(s => s.Id = "12345")
                .With(s => s.Created = new DateTime(2021, 2, 3))
                .Build();
            A.CallTo(() => _mongoDBService.GetApiKeyAsync("68869c32-d971-40f0-8b12-7392a153cd94")).Returns(apiKey);
            var command = new GetApiKeyQuery("68869c32-d971-40f0-8b12-7392a153cd94");

            // Act
            var result = await _getApiKeyQueryHandler.Handle(command, default);

            // Assert
            A.CallTo(() => _mongoDBService.GetApiKeyAsync("68869c32-d971-40f0-8b12-7392a153cd94")).MustHaveHappenedOnceExactly();
            Assert.Equal("68869c32-d971-40f0-8b12-7392a153cd94", result.Key);
        }

        [Fact]
        public async void GetApiKeyQueryHandler_should_return_ArgumentNullException_required_paramenter_null()
        {
            // Act Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _getApiKeyQueryHandler.Handle(new GetApiKeyQuery(null), default));
        }
    }
}
