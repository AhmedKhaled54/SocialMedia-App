using Core.Feature.Notifications.Comand.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Notifications.Comand.Validator
{
    public  class DeleteNotificationValidator:AbstractValidator<DeleteNotificationCommand>
    {
        public DeleteNotificationValidator()
        {
             RuleFor(c=>c.NotificationId)
                .NotEmpty().WithMessage("Notification Id Is Required")
                .GreaterThan(0).WithMessage("Notification Id Must Be Greater Than 0");
        }
    }
}
