using FluentAssertions;
using Restaurant.Application.Users;
using Restaurant.Domain.Contants;
using Xunit;

namespace Restaurant.Application.Users.Tests
{
    public class CurrentUserTests
    {

        [Theory()]
        [InlineData(UserRole.Admin)]
        [InlineData(UserRole.User)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRole.Admin, UserRole.User] , null , null);
            //act
            var isInRole = currentUser.IsInRole(roleName);
            //assert
            isInRole.Should().BeTrue();
        }
        
        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnTrue()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRole.Admin, UserRole.User] , null , null);
            //act
            var isInRole = currentUser.IsInRole(UserRole.Owner);
            //assert
            isInRole.Should().BeFalse();
        }
        
        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnTrue()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRole.Admin, UserRole.User] , null , null);
            //act
            var isInRole = currentUser.IsInRole(UserRole.Admin.ToLower());
            //assert
            isInRole.Should().BeFalse();
        }
    }
}