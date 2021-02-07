using FakeItEasy;
using FizzWare.NBuilder;
using Movie.Tests;
using Movies.Core.Interfaces;
using Movies.Core.Models;
using Movies.Feature.Queries;
using Xunit;

namespace Movies.Tests.Movies.Queries
{
    public class GetMovieFromSourceQueryHandlerTests : BaseTest
    {
        private readonly ISourceMovieService _sourceMovieService;
        private readonly GetMovieFromSourceQueryHandler _getMovieFromSourceQueryHandler;

        public GetMovieFromSourceQueryHandlerTests()
        {
            _sourceMovieService = A.Fake<ISourceMovieService>();
            _getMovieFromSourceQueryHandler = new GetMovieFromSourceQueryHandler(_sourceMovieService);
        }

        [Fact]
        public async void GetMovieFromSourceQueryHandler_should_return_movie()
        {
            // Arrange
            var movies = Builder<SourceMovieDto>.CreateNew()
                .With(s => s.Title = "Face/Off")
                .With(s => s.Year = "1997")
                .With(s => s.Rated = "R")
                .With(s => s.Released = "27 Jun 1997")
                .With(s => s.Runtime = "138 min")
                .With(s => s.Genre = "Action, Crime, Sci-Fi, Thriller")
                .With(s => s.Director = "John Woo")
                .With(s => s.Writer = "Mike Werb, Michael Colleary")
                .With(s => s.Actors = "John Travolta, Nicolas Cage, Joan Allen, Alessandro Nivola")
                .With(s => s.Plot = "In order to foil a terrorist plot")
                .With(s => s.Language = "English, Latin")
                .With(s => s.Country = "USA")
                .With(s => s.Awards = "Nominated for 1 Oscar. Another 11 wins & 21 nominations.")
                .With(s => s.Poster = "poster")
                .With(s => s.ImdbRating = "7.2")
                .With(s => s.ImdbVotes = "348,502")
                .With(s => s.ImdbID = "tt0119094")
            .Build();
            A.CallTo(() => _sourceMovieService.GetMovieAsync("Face/Off")).Returns(movies);

            // Act
            var result = await _getMovieFromSourceQueryHandler.Handle(new GetMovieFromSourceQuery() { Title = "Face/Off" }, default);

            // Assert
            A.CallTo(() => _sourceMovieService.GetMovieAsync("Face/Off")).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            Assert.Equal(movies.Title, result.Title);
            Assert.Equal(movies.Year, result.Year);
            Assert.Equal(movies.Rated, result.Rated);
            Assert.Equal(movies.Released, result.Released);
            Assert.Equal(movies.Runtime, result.Runtime);
            Assert.Equal(movies.Genre, result.Genre);
            Assert.Equal(movies.Director, result.Director);
            Assert.Equal(movies.Writer, result.Writer);
            Assert.Equal(movies.Actors, result.Actors);
            Assert.Equal(movies.Plot, result.Plot);
            Assert.Equal(movies.Language, result.Language);
            Assert.Equal(movies.Country, result.Country);
            Assert.Equal(movies.Awards, result.Awards);
            Assert.Equal(movies.Poster, result.Poster);
            Assert.Equal(movies.ImdbRating, result.ImdbRating);
            Assert.Equal(movies.ImdbVotes, result.ImdbVotes);
            Assert.Equal(movies.ImdbID, result.ImdbID);
        }
    }
}
