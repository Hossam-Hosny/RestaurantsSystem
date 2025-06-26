using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Infrastructure.Authorization.Requirments.Tests;

public class CreatedMultipleRestaurantsRequirementHandlerTests
{
    [Fact()]
    public async Task HandleRequirmentAsync_UserHasCreatedMultipleRestaurants_ShouldSecced()
    {
        // arrange 
        var userContextMock = new Mock<IUserContext>();

        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
        userContextMock.Setup(u => u.GetCurrentUser())
            .Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.Id
            },
            new()
            {
                OwnerId = currentUser.Id
            },
            new()
            {
                OwnerId = "2"
            },
        };

        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        restaurantRepositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(restaurants);

        var requirment = new CreatedMultipleRestaurantsRequirement(2);
        var handler = new CreatedMultipleRestaurantsRequirementHandler(userContextMock.Object
            , restaurantRepositoryMock.Object);

        var context = new AuthorizationHandlerContext([requirment], null, null);

        // act
        await handler.HandleAsync(context);

        // assert 
        context.HasSucceeded.Should().BeTrue();
    }

    [Fact()]
    public async Task HandleRequirmentAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail()
    {
        // arrange 
        var userContextMock = new Mock<IUserContext>();

        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
        userContextMock.Setup(u => u.GetCurrentUser())
            .Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.Id
            },
            new()
            {
                OwnerId = currentUser.Id
            },
            new()
            {
                OwnerId = "2"
            },
        };

        var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        restaurantRepositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(restaurants);

        var requirment = new CreatedMultipleRestaurantsRequirement(3);
        var handler = new CreatedMultipleRestaurantsRequirementHandler(userContextMock.Object
            , restaurantRepositoryMock.Object);

        var context = new AuthorizationHandlerContext([requirment], null, null);

        // act
        await handler.HandleAsync(context);

        // assert 
        context.HasSucceeded.Should().BeFalse();
        context.HasFailed.Should().BeTrue();
    }
}