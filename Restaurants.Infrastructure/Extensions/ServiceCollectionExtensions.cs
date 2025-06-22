using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirments;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Context;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{

    public static void AddInfrastructure(this IServiceCollection services,IConfiguration config )
    {
        var connectionString = config.GetConnectionString("LocalConnectionString");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging();
        });

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<AppDbContext>();


        services.AddScoped<IRestaurantSeeder,RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantsRepository>();
        services.AddScoped<IDishRepository, DishRepository>();
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.CreatedAtleast2Restaurants,builder=>builder.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)))
            .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "Egypt"))
            .AddPolicy(PolicyNames.AtLeast20,builder=>builder.AddRequirements(new MinimumAgeRequirements(20)));

        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementsHandler>();
        services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantsRequirementHandler>();

        services.AddScoped<IrestaurantAuthorizationService, restaurantAuthorizationService>();



    }




}
