using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests;

public class UpdateRestaurantCommandHandlerTests
{

    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IRestaurantRepository> _restaurantRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IrestaurantAuthorizationService> _restaurantAuthorizaionServiceMock;
    private readonly UpdateRestaurantCommandHandler _handler;

    public UpdateRestaurantCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _restaurantRepositoryMock = new Mock<IRestaurantRepository>();
        _mapperMock = new Mock<IMapper>();
        _restaurantAuthorizaionServiceMock = new Mock<IrestaurantAuthorizationService>();

        _handler = new UpdateRestaurantCommandHandler(_loggerMock.Object
            ,_restaurantRepositoryMock.Object
            ,_mapperMock.Object
            ,_restaurantAuthorizaionServiceMock.Object);
    }

    [Fact()]
    public async Task Handle_WithValidRequest_ShouldUpdateRestaurant()
    {
        // arrange
        var restaurantId = 1;
        var command = new UpdateRestaurantCommand()
        {
            Id = restaurantId,
            Name = "New Name",
            Description = "New Description",
            HasDelivery = true
        };
        var restaurant = new Restaurant()
        {
            Id = restaurantId,
            Name = "name",
            Description = "description",
            HasDelivery = false
        };

        _restaurantRepositoryMock.Setup(r=>r.GetRestaurantById(restaurantId))
            .ReturnsAsync(restaurant);
        _restaurantAuthorizaionServiceMock.Setup(m => m.Authorize(restaurant, Domain.Constants.ResourceOperation.update))
            .Returns(true);

        // act 

        await _handler.Handle(command, CancellationToken.None);

        // assert 

        _restaurantRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once);


    }

    [Fact]
    public async Task Handle_WithNotExistingRestaurant_ShouldThrowNotFoundException()
    {
        // arrange 
        var restaurantId = 2;
        var request = new UpdateRestaurantCommand
        {
            Id = restaurantId
        };

        _restaurantRepositoryMock.Setup(r => r.GetRestaurantById(restaurantId))
            .ReturnsAsync((Restaurant?)null);

        // act 
        Func<Task> action = async () => await _handler.Handle(request, CancellationToken.None);

        // assert
        await action.Should().ThrowAsync<NotFoundException>();

    }

    [Fact]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidenException()
    {

        // arrange 
        var restaurantId = 3;
        var request = new UpdateRestaurantCommand
        {
            Id = restaurantId
        };

        var existingRestaurant = new Restaurant
        {
            Id = restaurantId
        };

        _restaurantRepositoryMock.Setup(r => r.GetRestaurantById(restaurantId))
            .ReturnsAsync(existingRestaurant);


        _restaurantAuthorizaionServiceMock.Setup(a => a.Authorize(existingRestaurant, Domain.Constants.ResourceOperation.update))
            .Returns(false);


        // act 
        Func<Task> action = async () => await _handler.Handle(request, CancellationToken.None);

        // assert 
        await action.Should().ThrowAsync<ForbidException>();


    }


}