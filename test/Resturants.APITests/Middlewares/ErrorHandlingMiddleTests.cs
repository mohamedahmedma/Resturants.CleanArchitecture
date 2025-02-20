using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Exceptions;
using Xunit;
namespace Resturants.API.Middlewares.Tests
{
    public class ErrorHandlingMiddleTests
    {
        [Fact()]
        public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegateTest()
        {
            //arrange
            var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(logger.Object);
            var context = new DefaultHttpContext();
            var nextDelegateMock = new Mock<RequestDelegate>();

            //act
            await middleware.InvokeAsync(context , nextDelegateMock.Object);

            //assert
            nextDelegateMock.Verify(next => next.Invoke(context) ,Times.Once);
        }

        [Fact()]
        public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetSatatusCode404()
        {
            //arrange
            var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(logger.Object);
            var context = new DefaultHttpContext();
            var notFoundException = new NotFoundException(nameof(Restaurants), "1");
            //act

            await middleware.InvokeAsync(context , _ => throw notFoundException);
            //assert

            context.Response.StatusCode.Should().Be(404);
        }

        [Fact()]
        public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetSatatusCode403()
        {
            //arrange
            var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(logger.Object);
            var context = new DefaultHttpContext();
            var exception = new ForbidException();
            //act

            await middleware.InvokeAsync(context , _ => throw exception);
            //assert

            context.Response.StatusCode.Should().Be(403);
        }

        [Fact()]
        public async Task InvokeAsync_WhenGeneralExceptionThrown_ShouldSetSatatusCode500()
        {
            //arrange
            var logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(logger.Object);
            var context = new DefaultHttpContext();
            var exception = new Exception();
            //act

            await middleware.InvokeAsync(context , _ => throw exception);
            //assert

            context.Response.StatusCode.Should().Be(500);
        }


    }
}