
using Core.Feature.Authantication.command.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Validator
{
    public  class AddForgetPasswordValidator:AbstractValidator<ForgetPasswordCommand>
    {
        public AddForgetPasswordValidator()
        {
            Apply();
        }



        public  void Apply()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email Is Required !");
        }
    }
}
