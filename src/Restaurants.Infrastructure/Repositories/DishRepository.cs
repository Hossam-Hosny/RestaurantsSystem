using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Context;

namespace Restaurants.Infrastructure.Repositories;

internal class DishRepository(AppDbContext _db) : IDishRepository
{
    public async Task<int> CreateAsync(Dish entity)
    {
        _db.Dishes.Add(entity);
        await _db.SaveChangesAsync();
        
        return entity.Id;
        
    }

    public async Task DeleteAsync(IEnumerable<Dish> entities)
    {
        _db.Dishes.RemoveRange(entities);
        await _db.SaveChangesAsync();
    }
}
