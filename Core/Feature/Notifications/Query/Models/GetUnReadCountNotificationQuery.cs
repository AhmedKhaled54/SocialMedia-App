using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Notifications.Query.Models
{
    public  class GetUnReadCountNotificationQuery:IRequest<Response<int>>
    {
    }
}
