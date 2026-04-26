using Core.Feature.Authantication.command.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Validator
{
    public  class AddRegitserValidator:AbstractValidator<RegisterCommand>
    {

        public AddRegitserValidator()
        {
            ApplyValidator();
        }

        public void ApplyValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("UserName Is Required !");
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email Is Required ")
                .EmailAddress()
                .WithMessage("Please enter a valid email address (e.g., user@example.com)");

            RuleFor(c => c.BirthDate).NotEmpty()
                .WithMessage("BirthDate Is Required")
                .Must(date => DateTime.Today.Year - date.Year >= 18)
                .WithMessage("You must be at least 18 years old.");

            RuleFor(c => c.Gender).NotEmpty().WithMessage("Gender Is Required ")
                .IsInEnum().WithMessage("Invalid gender value");

            RuleFor(c => c.Password).NotEmpty().WithMessage("Password Is Required !");
            RuleFor(c => c.ConfirmPassword).NotEmpty().WithMessage("Please Confirm Password !");
               
        }
               

    }
}
