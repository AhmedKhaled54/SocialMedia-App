using Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Comments.Command.Models
{
    public  class EditCommentCommand:IRequest<Response<string>>
    {
        public int CommentId { get; set; }
        public string? Content { get; set; }
        public List<IFormFile>?Files { get; set; }
        public List<int>? MediaRemoved { get; set; }


    }
}
