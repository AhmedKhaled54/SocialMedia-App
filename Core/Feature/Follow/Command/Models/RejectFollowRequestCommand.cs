using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Follow.Command.Models
{
    public  class RejectFollowRequestCommand:IRequest<Response<string>>
    {
        public int RequestId {  get; set; }
    }
}
