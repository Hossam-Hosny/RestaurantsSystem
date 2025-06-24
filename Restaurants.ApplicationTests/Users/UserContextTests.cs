using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constants;
using System.Security.Claims;
using Xunit;

namespace Restaurants.Application.Users.Tests;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        // arrange

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var dateOfBirth = new DateOnly(1999,01,01);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier,"1"),
            new(ClaimTypes.Email,"test@test.com"),
            new(ClaimTypes.Role,UserRoles.Admin),
            new(ClaimTypes.Role,UserRoles.User),
            new("Nationality","Egypt"),
            new("DateOfBirth",dateOfBirth.ToString("yyyy-MM-dd")),
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims,"test"));
        httpContextAccessorMock.Setup(x=>x.HttpContext).Returns(new DefaultHttpContext()
        {
            User =user
        });

        var userContext = new UserContext(httpContextAccessorMock.Object);

        // arrange

        var currentUser = userContext.GetCurrentUser();

        // assert

        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@test.com");
        currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
        currentUser.Nationality.Should().Be("Egypt");
        currentUser.DateOfBirth.Should().Be(dateOfBirth);


    }

    [Fact]
    public void GetCurrentUser_WithUserContextNotPresent_ShouldThrowsInvalidOperationException()
    {
        // arrange

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);
        var userContext = new UserContext(httpContextAccessorMock.Object);


        // act 

        Action action = () => userContext.GetCurrentUser();


        // assert

        action.Should().Throw<InvalidOperationException>();



    }

}