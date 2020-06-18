using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validations
{
   public class ProductValidator :   AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name must be filled out");
            RuleFor(c => c.Description).NotEmpty().WithMessage("Description name must be filled out");
            RuleFor(c => c.Price).NotEmpty().WithMessage("Price name must be filled out")
                .Must(value => value >= 0).WithMessage("Price must not negative");
            RuleFor(c => c.Stock).NotEmpty().WithMessage("Stock name must be filled out")
                .Must(value=> value >= 0).WithMessage("Stock must not negative");
        }
    }
}
