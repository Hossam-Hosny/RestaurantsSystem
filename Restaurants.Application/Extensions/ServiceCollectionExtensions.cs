using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extensions;

public static  class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection _services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        _services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        _services.AddAutoMapper(applicationAssembly);

        _services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        _services.AddScoped<IUserContext, UserContext>();
        _services.AddHttpContextAccessor();
    }
}
