using Core.Feature.conversation.Command.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.conversation.Command.Validator
{
    public  class DirectMessageCommandValidator:AbstractValidator<DirectMessageCommand>
    {
        public DirectMessageCommandValidator()
        {
             
        }


        public void Apply()
        {

            RuleFor(c => c.Message).NotEmpty()
                .MaximumLength(400)
                .WithMessage("Message Is Too Long !");

        }
    }
}
