using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Context;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(AppDbContext _db) : IRestaurantSeeder
{

    public async Task Seed()
    {
        if (await _db.Database.CanConnectAsync())
        {
            if (!_db.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                _db.Restaurants.AddRange(restaurants);
                await _db.SaveChangesAsync();
            }

            if (!_db.Roles.Any())
            {
                var roles = GetRoles();
                _db.AddRange(roles);
                await _db.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new (UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new (UserRoles.Admin) 
                { NormalizedName = UserRoles.Admin.ToUpper() }
            ];
        return roles;
    }


    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants = [

            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description="This is description of Fast Food Restaurant",
                HasDelivery= true,
                Dishes =[
                    new(){
                        Name=" Chicken",
                         Description = "Fired Chicken",
                         Price = 10
                    },
                    new(){
                        Name = "chicken nuggets",
                        Description="chicken nuggets description",
                        Price = 5
                    }
                    ],
                Address = new(){
                    City = "london",
                    Street="Cork st 5",
                    PostalCode = "WC2N 5DU"
                },
                ContactEmail="KFC@gmail.com",
                ContactNumber= "+97568492997"

            },
            new()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description="This is description of Fast Food Restaurant",
                HasDelivery= true,
                Dishes =[
                    new(){
                        Name=" Burger",
                         Description = "Fired Burger",
                         Price = 11
                    },
                    new(){
                        Name = "chicken nuggets",
                        Description="chicken nuggets description",
                        Price = 6
                    }
                    ],
                Address = new(){
                    City = "New Yourk",
                    Street="Yourk st 5",
                    PostalCode = "M street 5DU"
                },
                ContactEmail="MC@gmail.com",
                ContactNumber= "+97500002997"

            }

            ];
        return restaurants;
    }
}
