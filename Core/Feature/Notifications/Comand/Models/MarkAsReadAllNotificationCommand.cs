using Core.Bases;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Notifications.Comand.Models
{
    public  class MarkAsReadAllNotificationCommand: IRequest<Response<bool>>
    {
    }
}
