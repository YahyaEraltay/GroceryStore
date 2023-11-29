using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryProject.Models.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotNull().WithMessage("Please provide a category name").NotEmpty().WithMessage("Please provide a category name");
            RuleFor(x => x.CategoryName).Length(2, 20).WithMessage("Please enter length between 2 - 20");
            RuleFor(x => x.CategoryName).Must(BeText).WithMessage("Category name must be a text");

            RuleFor(x=>x.CategoryDescription).NotNull().WithMessage("Please provide a category description").NotEmpty().WithMessage("Please provide a category description");
            RuleFor(x => x.CategoryDescription).Length(2, 50).WithMessage("Please enter length between 2 - 50");
            RuleFor(x => x.CategoryDescription).Must(BeText).WithMessage("Category name must be a text");
        }
        private bool BeText(string value)
        {
            return !string.IsNullOrEmpty(value) && !value.Any(char.IsDigit);
        }
    }
}
