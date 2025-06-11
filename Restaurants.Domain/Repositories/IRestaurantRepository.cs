using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetRestaurantById(int id);
    Task<int> CreateAsync(Restaurant restaurant);
    Task DeleteAsync(Restaurant model);
    Task SaveChanges();
}
