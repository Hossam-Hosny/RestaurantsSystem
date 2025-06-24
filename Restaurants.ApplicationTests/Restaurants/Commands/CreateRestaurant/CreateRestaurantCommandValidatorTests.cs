using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "test",
            Description = "This is description",
            Category = "Egyption",
            ContactEmail = "test@test.com",
            PostalCode = "22-444",
        };

        var validator = new CreateRestaurantCommandValidator();


        // act 

        var result = validator.TestValidate(command);


        // assert

        result.ShouldNotHaveAnyValidationErrors();



    }

    [Fact]
    public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
    {
        // arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "tt",
            Description = "",
            Category = "Egypton",
            ContactEmail = "test.test.com",
            PostalCode = "22-44",
        };

        var validator = new CreateRestaurantCommandValidator();


        // act 

        var result = validator.TestValidate(command);


        // assert

        result.ShouldHaveValidationErrorFor(c=>c.Name);
        result.ShouldHaveValidationErrorFor(c=>c.Description);
        result.ShouldHaveValidationErrorFor(c=>c.Category);
        result.ShouldHaveValidationErrorFor(c=>c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c=>c.PostalCode);



    }


    [Theory()]
    [InlineData("Indian")]
    [InlineData("Palastinain")]
    [InlineData("Egyption")]
    [InlineData("Italian")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        // arrange

        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { Category = category };

        // act 
        var result = validator.TestValidate(command);
        // assert

        result.ShouldNotHaveValidationErrorFor(c=>c.Category);


    }

    [Theory()]
    [InlineData("1-111")]
    [InlineData("1-11")]
    
    [InlineData("1-1111")]
    [InlineData("1-1-11")]
    [InlineData("1-11-1")]
    [InlineData("-11-1")]
    [InlineData("1-1-1-1")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string prop)
    {
        // arrange

        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { PostalCode = prop };

        // act 
        var result = validator.TestValidate(command);
        // assert

        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }




}