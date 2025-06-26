
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Context;
using System.Linq.Expressions;

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
    public async Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize, string? SortBy, SortDirection SortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery =  _db.Restaurants
            .Include(r => r.Dishes)
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
            || r.Description.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync();

        if (SortBy is not null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                {nameof(Restaurant.Name),r=>r.Name },
                {nameof(Restaurant.Description),r=>r.Description },
                {nameof(Restaurant.Category),r=>r.Category },
            };

            var selectedColumn = columnsSelector[SortBy];

            baseQuery = SortDirection == SortDirection.Ascending
                ?baseQuery.OrderBy(selectedColumn)
                :baseQuery.OrderByDescending(selectedColumn);

        }





        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants,totalCount);
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
