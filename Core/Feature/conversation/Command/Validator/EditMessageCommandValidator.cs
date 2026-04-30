using Core.Feature.conversation.Command.Models;
using FluentValidation;
using Core.Feature.conversation.Command.Models;

namespace Core.Feature.conversation.Command.Validator
{
    public class EditMessageCommandValidator : AbstractValidator<EditMessageCommand>
    {
        public EditMessageCommandValidator()
        {
            Apply();
           
        }

        public void Apply()
        {
            RuleFor(c => c.MessageId)
                 .NotEmpty()
                 .WithMessage("MessageId is required.");

            RuleFor(c => c.Body).NotEmpty()
                .MaximumLength(200)
                .WithMessage("Body is too long.");
        }
    }
}
