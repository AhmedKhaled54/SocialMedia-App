using Core.Bases;
using Data.Enums.Like;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Posts.Command.Models
{
    public  class ReactToPostCommand:IRequest<Response<string>>
    {
        public int PostId { get; set; }
        public LikeType ReactType { get; set; }


    }
}
