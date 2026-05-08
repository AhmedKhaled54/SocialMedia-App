using Core.Feature.Story.Command.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Story.Command.Validator
{
    public  class AddStoryCommandValidator:AbstractValidator<AddStoryCommand>
    {
        public AddStoryCommandValidator()
        {
            RuleFor(c => c.Files).NotEmpty().WithMessage("Please select at least one file.")
                .Must(c => c.Count <= 2).WithMessage("You can upload a maximum of 2 files.");

        }

        
    }
}
