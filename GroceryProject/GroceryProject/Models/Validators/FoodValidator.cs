using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryProject.Models.Validators
{
    public class FoodValidator: AbstractValidator<Food>
    {
        public FoodValidator()
        {
            RuleFor(x => x.FoodName).NotNull().WithMessage("Please provide a food name").NotEmpty().WithMessage("Please provide a food name");
            RuleFor(x => x.FoodName).Length(2, 20).WithMessage("Please enter length between 2 - 20");
            RuleFor(x => x.FoodName).Must(BeText).WithMessage("Food name must be a text");

            RuleFor(x=>x.FoodDescription).NotNull().WithMessage("Please provide a food description").NotEmpty().WithMessage("Please provide a food description");
            RuleFor(x => x.FoodDescription).Length(5, 50).WithMessage("Please enter length between 5 - 50");

            RuleFor(x => x.FoodPrice).NotNull().WithMessage("Please provide a food price").NotEmpty().WithMessage("Please provide a food price");
            RuleFor(x => x.FoodPrice).Must(BeNumeric).WithMessage("Food price must be a numeric value");

            RuleFor(x => x.FoodStock).NotNull().WithMessage("Please provide a food stock").NotEmpty().WithMessage("Please provide a food stock");
            RuleFor(x => x.FoodStock).Must(BeNumericStock).WithMessage("Food price must be a numeric value");

            RuleFor(x => x.CategoryID).NotNull().WithMessage("Please select a food category").NotEmpty().WithMessage("Please select a food category");

        }
        private bool BeNumeric(double value)
        {
            return double.TryParse(value.ToString(), out _);
        }
        private bool BeNumericStock(int value)
        {
            return int.TryParse(value.ToString(), out _);
        }
        private bool BeText(string value)
        {
            return !string.IsNullOrEmpty(value) && !value.Any(char.IsDigit);
        }
    }
}
