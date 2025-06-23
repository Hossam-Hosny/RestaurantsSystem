
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Context;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(AppDbContext _db)
    : IRestaurantRepository
{
    public async Task<int> CreateAsync(Restaurant restaurant)
    {
        await _db.Restaurants.AddAsync(restaurant);
        await _db.SaveChangesAsync();

        return restaurant.Id;

    }

    public async Task DeleteAsync(Restaurant model)
    {
         _db.Remove(model);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await _db.Restaurants
            .Include(r => r.Dishes)
            .ToListAsync();
        return restaurants;
    }
    public async Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var restaurants = await _db.Restaurants
            .Include(r => r.Dishes)
            .Where(r=> searchPhraseLower == null ||( r.Name.ToLower().Contains(searchPhraseLower)
            || r.Description.ToLower().Contains(searchPhraseLower)))
            .ToListAsync();
        return restaurants;
    }

    public async Task<Restaurant?> GetRestaurantById(int id)
    {
        var restaurant = await _db.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);
        return restaurant;
    }

    public async Task SaveChanges()
    {
         await _db.SaveChangesAsync();
    }
}
