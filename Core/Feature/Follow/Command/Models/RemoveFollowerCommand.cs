using Core.Bases;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Follow.Command.Models
{
    public  class RemoveFollowerCommand:IRequest<Response<string>>
    {
        public int FollowerId { get; set; }
    }
}
