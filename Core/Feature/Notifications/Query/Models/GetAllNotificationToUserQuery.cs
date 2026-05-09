using Core.Bases;
using Core.Feature.Notifications.Query.Results;
using Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Notifications.Query.Models
{
    public  class GetAllNotificationToUserQuery:IRequest<Response<Pagination<GetNotificationQueryResult>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

       
    }
}
