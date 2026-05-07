using Core.Feature.Posts.Command.Models;
using Data.Enums.Like;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Posts.Command.Validator
{
    public  class ReactPostCommandValidator:AbstractValidator<ReactToPostCommand>
    {
        public ReactPostCommandValidator()
        {
            Apply();
             
        }


        public void Apply()
        {
            RuleFor(c => c.PostId).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("PostId Greater Than (zero)");
            RuleFor(c => c.ReactType).IsInEnum().WithMessage("Invalid ReactionType");
            //RuleFor(c => c.ReactType)
              //  .Must(type => Enum.IsDefined(typeof(LikeType), type));//must 1,2,3.....
        
        }
    }
}
