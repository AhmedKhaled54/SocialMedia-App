using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Notifications.Comand.Models
{
    public  class MarkAsReadNotificationCommand:IRequest<Response<bool>>
    {
        public int NotificationId { get; set; }
    }
}
