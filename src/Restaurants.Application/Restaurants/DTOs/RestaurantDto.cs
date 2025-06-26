using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; } = default!;

    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }


    public List<DishDto> Dishes { get; set; } = [];





}
