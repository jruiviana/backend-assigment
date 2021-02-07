using System.Threading.Tasks;
using Xunit;
using System;
using System.ComponentModel.DataAnnotations;
using FakeItEasy;
using Movie.Tests;
using Movies.API.Middleware;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Movies.Tests.MiddlewareTests
{
    public class ErrorHandlingMiddlewareTests : BaseTest
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly DefaultHttpContext _httpContext;
        private readonly ErrorHandlingMiddleware _errorHandlingMiddleware;

        public ErrorHandlingMiddlewareTests()
        {
            _next = A.Fake<RequestDelegate>();
            _httpContext = new DefaultHttpContext();
            _logger = A.Fake<ILogger<ErrorHandlingMiddleware>>();
            _errorHandlingMiddleware = new ErrorHandlingMiddleware(_next, _logger);
        }

        [Fact]
        public async Task Invoke_must_call_requestDelegate_invoker()
        {
            //Arrange
            A.CallTo(() => _next.Invoke(A<HttpContext>.Ignored)).Returns(Task.FromResult(1));

            //Act
            await _errorHandlingMiddleware.Invoke(_httpContext);

            //Assert
            A.CallTo(() => _next.Invoke(A<HttpContext>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public async Task Invoke_must_log_warning_and_set_HttpStatusCode_badRequest_on_ValidationException()
        {
            //Arrange
            _httpContext.Request.Path = new PathString("/test");
            _httpContext.Request.QueryString = new QueryString("?query");
            _httpContext.Request.Method = "Get";
            A.CallTo(() => _next.Invoke(A<HttpContext>.Ignored)).ThrowsAsync(new ValidationException("Validation error"));

            //Act
            await _errorHandlingMiddleware.Invoke(_httpContext);

            //Assert
            var message = $"Bad Request: Validation error - {_httpContext.Request.Method} {_httpContext.Request.Path + _httpContext.Request.QueryString}.";
            AssertLogMethod(message, typeof(ValidationException), LogLevel.Warning);
            Assert.Equal(400, _httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_must_log_warning_and_set_HttpStatusCode_InternalServerError_on_any_other_exception()
        {
            //Arrange
            _httpContext.Request.Path = new PathString("/test");
            _httpContext.Request.QueryString = new QueryString("?query");
            _httpContext.Request.Method = "Get";
            A.CallTo(() => _next.Invoke(A<HttpContext>.Ignored)).ThrowsAsync(new ArgumentNullException("Argument missing"));

            //Act
            await _errorHandlingMiddleware.Invoke(_httpContext);

            //Assert
            var message = $"Error: Value cannot be null. (Parameter 'Argument missing').";
            AssertLogMethod(message, typeof(ArgumentNullException), LogLevel.Error);
            Assert.Equal(500, _httpContext.Response.StatusCode);
        }

        private void AssertLogMethod(string message, Type type, LogLevel logLevel)
        {
            A.CallTo(_logger).Where(x => x.Method.Name.Equals("Log")
               && x.Arguments.Get<LogLevel>(0) == logLevel
               && x.Arguments.Get<object>(2).ToString().Equals(message)
               && x.Arguments.Get<object>(3).GetType() == type)
               .MustHaveHappenedOnceExactly();
        }
    }
}
