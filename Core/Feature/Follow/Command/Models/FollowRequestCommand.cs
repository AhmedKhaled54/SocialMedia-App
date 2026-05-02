using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Follow.Command.Models
{
    public  class FollowRequestCommand:IRequest<Response<string>>
    {
        public int ReceiveId  {  get; set; }
    }
}
