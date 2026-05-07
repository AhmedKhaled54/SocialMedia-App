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
    public  class AddCommentCommand:IRequest<Response<string>>
    {
        public int PostId { get; set; }
        public string? content { get; set; }
        public List<IFormFile>? media { get; set; }

    }
}
