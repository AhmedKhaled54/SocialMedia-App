using Core.Feature.Comments.Command.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Comments.Command.Validator
{
    public  class AddRactToCommantCommandvalidation:AbstractValidator<ReactToCommentCommand>
    {
        public AddRactToCommantCommandvalidation()
        {
            Apply();
             
        }


        public void Apply()
        {
            RuleFor(c => c.CommentId).NotEmpty().GreaterThan(0).WithMessage("Comment Is Required ");
            RuleFor(c => c.React).IsInEnum().WithMessage("Invalid Reaction Type!");
        }
    }
}
