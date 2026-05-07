using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Comments.Command.Models
{
    public  class DeleteCommentCommand:IRequest<Response<string>>
    {
        public int CommentId { get; set; }

       
    }
}
