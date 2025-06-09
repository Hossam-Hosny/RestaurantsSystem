using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Validators;

public class RegisterRestaurantValidator : AbstractValidator<RegisterRestaurantDto>
{
    private readonly List<string> _validCategories = ["Italian", "Egyption", "Palastinain", "Indian"];
    public RegisterRestaurantValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 50);

        RuleFor(dto => dto.Description)
            .NotEmpty()
            .WithMessage("Description is Required {Hello from Fluent Validation}");
           

        RuleFor(dto => dto.Category)
            .Custom((value, context) =>
            {
                var isValidCategory = _validCategories.Contains(value);
                if (!isValidCategory)
                {
                    context.AddFailure("Category", "Please choose from Category list {Hello From Fluent Validation}");
                }
            });
            

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please Provide a valid email address");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please Provide a valid postal code  (xx-xxx)");
    }

}
