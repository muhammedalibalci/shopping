using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validations
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(c => c.ShipName).NotEmpty().WithMessage("Description name must be filled out");
            RuleFor(c => c.ShipAddress).NotEmpty().WithMessage("ShipAddress name must be filled out");
            RuleFor(c => c.Amount).NotEmpty().WithMessage("Amount name must be filled out")
                .Must(value => value >= 0).WithMessage("Price must not negative");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email name must be filled out");
            RuleFor(c => c.Phone).NotEmpty().WithMessage("Phone name must be filled out");
        }
    }
}
