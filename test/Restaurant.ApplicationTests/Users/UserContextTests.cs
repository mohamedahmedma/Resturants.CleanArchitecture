using Xunit;
using Restaurant.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Restaurant.Domain.Contants;
using FluentAssertions;

namespace Restaurant.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            //arrange

            var dateOfBirth = new DateOnly(2004, 4, 14);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var userContext = new UserContext(httpContextAccessorMock.Object);

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier ,"1" ),
                new(ClaimTypes.Email ,"test@test.com" ),
                new(ClaimTypes.Role ,UserRole.Admin ),
                new(ClaimTypes.Role ,UserRole.User ),
                new("Nationality" ,"German" ),
                new("DateOfBirth" ,dateOfBirth.ToString("yyyy-MM-dd"))

            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims , "Test"));

            httpContextAccessorMock.Setup( x=>x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });
            // act
            var currentUser = userContext.GetCurrentUser();

            //asset
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().ContainInOrder(UserRole.Admin , UserRole.User );
            currentUser.Nationality.Should().Be("German");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);
            
        }

        [Fact]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            // Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);
            var userContext = new UserContext(httpContextAccessorMock.Object);

            //act 
            Action action = () => userContext.GetCurrentUser();

            // assert
            action.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("User context is not present");
        }
    }
}