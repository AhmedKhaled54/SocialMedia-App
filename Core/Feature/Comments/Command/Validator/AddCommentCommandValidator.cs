using Core.Feature.Comments.Command.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Comments.Command.Validator
{
    public  class AddCommentCommandValidator:AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidator()
        {
            Apply();
             
        }


        public void Apply()
        {
            RuleFor(c => c.PostId).NotEmpty().WithMessage("Incorrect Comment The Post ");
            RuleFor(c => c.PostId).GreaterThan(0).WithMessage("PostId Is Required !");

            RuleFor(c => c)
                .Must(x => !string.IsNullOrEmpty(x.content) ||
                (x.media != null && x.media.Any()))
                .WithMessage("Must Comment Provide Content Or at least one media file !");
           
         }
    }
}
