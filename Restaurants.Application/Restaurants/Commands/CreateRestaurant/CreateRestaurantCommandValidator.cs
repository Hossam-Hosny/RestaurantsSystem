using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> _validCategories = ["Italian", "Egyption", "Palastinain", "Indian"];
    public CreateRestaurantCommandValidator()
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
