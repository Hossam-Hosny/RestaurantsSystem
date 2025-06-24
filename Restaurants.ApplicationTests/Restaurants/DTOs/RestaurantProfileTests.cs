using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Restaurants.DTOs.Tests;

public class RestaurantProfileTests
{
    private IMapper _mapper;

    public RestaurantProfileTests()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<RestaurantProfile>();
        });

         _mapper = configuration.CreateMapper();
    }


    [Fact()]
    public void CreateMap_FromRestaurantToRestaurantDto_ShouldMapsCorrectly()
    {
        // arrange

     
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "test",
            Category="this is category",
            ContactEmail = "test@test.com",
            Description = "this is description",
            HasDelivery = true,
            ContactNumber = "123456789",
            Address = new Address
            {
                Street = "street name",
                City = "city name",
                PostalCode = "12-345"
            }
        };

        // act 
        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        // assert 
        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurant.Id);
        restaurantDto.Name.Should().Be(restaurant.Name);
        restaurantDto.Description.Should().Be(restaurant.Description);
        restaurantDto.Category.Should().Be(restaurant.Category);
        restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurantDto.City.Should().Be(restaurant.Address.City);
        restaurantDto.Street.Should().Be(restaurant.Address.Street);
        restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);



    }


    [Fact()]
    public void CreateMap_FromCreateRestaurantCommandToRestaurant_ShouldMapsCorrectly()
    {
        // arrange

      
        var command = new CreateRestaurantCommand
        {
            
            Name = "test",
            Category = "this is category",
            ContactEmail = "test@test.com",
            Description = "this is description",
            HasDelivery = true,
            ContactNumber = "123456789",
            Street = "street name",
            City = "city name",
            PostalCode = "12-345"
        };

        // act 
        var restaurant = _mapper.Map<Restaurant>(command);

        // assert 
        restaurant.Should().NotBeNull();

        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.Category.Should().Be(command.Category);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);

        restaurant.Address.Should().NotBeNull();
        restaurant.Address.City.Should().Be(command.City);
        restaurant.Address.Street.Should().Be(command.Street);
        restaurant.Address.PostalCode.Should().Be(command.PostalCode);



    }



    [Fact()]
    public void CreateMap_FromUpdateRestaurantCommandToRestaurant_ShouldMapsCorrectly()
    {
        // arrange


        var command = new UpdateRestaurantCommand
        {

            Name = "test",
            Description = "this is description",
            HasDelivery = true,
           
        };

        // act 
        var restaurant = _mapper.Map<Restaurant>(command);

        // assert 
        restaurant.Should().NotBeNull();

        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);

        restaurant.HasDelivery.Should().Be(command.HasDelivery);





    }



}