using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("First name must be filled out");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Last name must be filled out");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password name must be filled out")
                .Must(d => d.Length >= 6).WithMessage("Passowrd must be 6 to 20 character"); ;
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email name must be filled out")
                .EmailAddress().WithMessage("A valid email is required"); 
        }
    }
}
