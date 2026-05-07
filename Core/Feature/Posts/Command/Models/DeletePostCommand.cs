using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Posts.Command.Models
{
    public  class DeletePostCommand:IRequest<Response<bool>>
    {
        public int PostId { get; set; }

        public DeletePostCommand(int postId)
        {
            PostId = postId;
        }
    }
}
