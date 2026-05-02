using Core.Bases;
using MediatR;
using Microsoft.OpenApi.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Follow.Command.Models
{
    public  class AcceptFollowRequestCommand:IRequest<Response<string>>
    {
        public int RequestID { get; set; }
    }
}
