using FakeItEasy;
using Movie.Tests;
using Movies.Core.Interfaces;
using Movies.Core.Models;
using Movies.Feature.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Movies.Tests.Movies.Queries
{
    public class SourceMovieServiceTests : BaseTest
    {
        private readonly IHttpHandler _httpHandler;
        private readonly IMovieSources _sources;
        private readonly SourceMovieService _sourceMovieService;

        public SourceMovieServiceTests()
        {
            _httpHandler = A.Fake<IHttpHandler>();
            _sources = new MovieSources
            {
                Sources = new List<string>() { "http://www.omdbapi.com/?i=tt3896198&apikey=e3ec1445&t={0}" }
            };
            _sourceMovieService = new SourceMovieService(_httpHandler, _sources);

        }

        [Fact]
        public async void GetMovieAsync_should_return_movie()
        {
            // Arrange
            var json = "{'Title':'Star Wars: Episode IV - A New Hope','Year':'1977','Rated':'PG','Released':'25 May 1977','Runtime':'121 min','Genre':'Action, Adventure, Fantasy, Sci-Fi','Director':'George Lucas'," +
                "'Writer':'George Lucas','Actors':'Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing','Plot':'Luke Skywalker joins.','Language':'English','Country':'USA','Awards':'Won 6 Oscars. Another 52 wins & 29 nominations.'," +
                "'Poster':'https://m.media-amazon.com/images/M/MV5BNzVlY2MwMjktM2E4OS00Y2Y3LWE3ZjctYzhkZGM3YzA1ZWM2XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX300.jpg'," +
                "'Ratings':[{'Source':'Internet Movie Database','Value':'8.6/10'},{'Source':'Rotten Tomatoes','Value':'92%'},{'Source':'Metacritic','Value':'90/100'}]," +
                "'Metascore':'90','imdbRating':'8.6','imdbVotes':'1,227,656','imdbID':'tt0076759','Type':'movie','DVD':'N/A','BoxOffice':'$460,998,507','Production':'Lucasfilm Ltd.','Website':'N/A','Response':'True'}";
            HttpContent content = new StringContent(json);
            A.CallTo(() => _httpHandler.GetAsync(A<string>.Ignored))
                .Returns(new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = content });

            // Act
            var result = await _sourceMovieService.GetMovieAsync("a");

            // Assert
            A.CallTo(() => _httpHandler.GetAsync(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            Assert.Equal("Star Wars: Episode IV - A New Hope", result.Title);
            Assert.Equal("1977", result.Year);
            Assert.Equal("PG", result.Rated);
            Assert.Equal("25 May 1977", result.Released);
            Assert.Equal("121 min", result.Runtime);
            Assert.Equal("Action, Adventure, Fantasy, Sci-Fi", result.Genre);
            Assert.Equal("George Lucas", result.Director);
            Assert.Equal("George Lucas", result.Writer);
            Assert.Equal("Mark Hamill, Harrison Ford, Carrie Fisher, Peter Cushing", result.Actors);
            Assert.Equal("Luke Skywalker joins.", result.Plot);
            Assert.Equal("English", result.Language);
            Assert.Equal("USA", result.Country);
            Assert.Equal("Won 6 Oscars. Another 52 wins & 29 nominations.", result.Awards);
            Assert.Equal("https://m.media-amazon.com/images/M/MV5BNzVlY2MwMjktM2E4OS00Y2Y3LWE3ZjctYzhkZGM3YzA1ZWM2XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SX300.jpg", result.Poster);
            Assert.Equal("8.6", result.ImdbRating);
            Assert.Equal("1,227,656", result.ImdbVotes);
            Assert.Equal("tt0076759", result.ImdbID);
        }

        [Fact]
        public async void GetMovieAsync_should_return_null_when_movie_not_founded()
        {
            // Arrange
            var json = "{'Response':'False','Error':'Movie not found!'}";
            HttpContent content = new StringContent(json);
            A.CallTo(() => _httpHandler.GetAsync(A<string>.Ignored))
                .Returns(new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = content });

            //Act Assert
            var result = await _sourceMovieService.GetMovieAsync("a");

            // Assert
            A.CallTo(() => _httpHandler.GetAsync(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Null(result);
        }
    }
}
