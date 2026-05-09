using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Notifications.Comand.Models
{
    public  class DeleteNotificationCommand:IRequest<Response<string>>
    {
        public int NotificationId { get; set; }

        public DeleteNotificationCommand(int notificationId)
        {
            NotificationId = notificationId;
        }
    }
}
