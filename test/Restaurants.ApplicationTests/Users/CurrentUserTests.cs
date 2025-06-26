using FluentAssertions;
using Restaurants.Domain.Constants;
using Xunit;


namespace Restaurants.Application.Users.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.Owner)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // arrange 
        var currentUser = new CurrentUser("1","test@test.com",[UserRoles.Admin,UserRoles.Owner],null,null);

        // act 

        var isInRole = currentUser.IsInRole(roleName);

        // assert

        isInRole.Should().BeTrue();


    }
    
    [Fact()]
    public void IsInRole_WithNotMatchingRole_ShouldReturnFalse()
    {
        // arrange 
        var currentUser = new CurrentUser("1","test@test.com",[UserRoles.Admin,UserRoles.Owner],null,null);

        // act 

        var isInRole = currentUser.IsInRole(UserRoles.User);

        // assert

        isInRole.Should().BeFalse();


    }

    
    
    [Fact()]
    public void IsInRole_WithNotMatchingRoleCase_ShouldReturnFalse()
    {
        // arrange 
        var currentUser = new CurrentUser("1","test@test.com",[UserRoles.Admin,UserRoles.Owner],null,null);

        // act 

        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        // assert

        isInRole.Should().BeFalse();


    }








}