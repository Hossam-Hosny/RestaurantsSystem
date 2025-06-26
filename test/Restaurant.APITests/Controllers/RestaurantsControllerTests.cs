using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurant.APITests;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Restaurant.API.Controllers.Tests;

public class RestaurantsControllerTests:IClassFixture<WebApplicationFactory<Program>>
{

    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IRestaurantRepository> _restaurantRepositoryMock = new();


    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantRepository),
                    _ => _restaurantRepositoryMock.Object));
            });
        });
    }




    [Fact()]
    public async Task Getall_ForValidRquest_ShouldReturns200Ok()
    {
        // arrange 
        var client = _factory.CreateClient();

        // act 
       var result = await client.GetAsync("api/Restaurants/get-all-restaurants?pageNumber=1&pageSize=5");

        // assert 
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);



    }

  
    [Fact()]
    public async Task Getall_ForInValidRquest_ShouldReturns400Badrequest()
    {
        // arrange 
        var client = _factory.CreateClient();

        // act 
       var result = await client.GetAsync("api/Restaurants/get-all-restaurants");

        // assert 
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);



    }


    [Fact]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {
        // arrange 
        var id = 12232;

        _restaurantRepositoryMock.Setup(m => m.GetRestaurantById(id))
            .ReturnsAsync((Restaurants.Domain.Entities.Restaurant?)null);

        var client = _factory.CreateClient();

        // act 
        var response = await client.GetAsync($"api/Restaurants/GetRestaurant/{id}");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }



    [Fact]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {
        // arrange 
        var id = 1;
        var restaurant = new Restaurants.Domain.Entities.Restaurant()
        {
            Id = id,
            Name = "test restaurant",
            Description = "test restaurant description"
        };


        _restaurantRepositoryMock.Setup(m => m.GetRestaurantById(id))
            .ReturnsAsync(restaurant);

        var client = _factory.CreateClient();

        // act 
        var response = await client.GetAsync($"api/Restaurants/GetRestaurant/{id}");
        var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

        
        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        restaurantDto.Should().NotBeNull();
        restaurantDto.Name.Should().Be("test restaurant");
        restaurantDto.Description.Should().Be("test restaurant description");

    }




}