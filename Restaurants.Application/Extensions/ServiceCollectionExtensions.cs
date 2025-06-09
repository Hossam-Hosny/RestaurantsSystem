using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Extensions;

public static  class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection _services)
    {
        _services.AddScoped<IRestaurantsService, RestaurantsService>();

        _services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
    }
}
