using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Restaurant.API.Controllers.Tests;

public class RestaurantsControllerTests:IClassFixture<WebApplicationFactory<Program>>
{

    private readonly WebApplicationFactory<Program> _factory;

    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
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

  
}