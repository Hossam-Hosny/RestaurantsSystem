namespace Restaurants.Application.Restaurants.DTOs;

public class RegisterRestaurantDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; } = default!;
    public string? ContactEmail { get; set; } = default!;
    public string? ContactNumber { get; set; } = default!;


    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}
