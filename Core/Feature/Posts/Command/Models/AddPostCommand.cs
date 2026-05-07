using Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Posts.Command.Models
{
    public  class AddPostCommand:IRequest<Response<string>>
    {
        public string? Caption { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}
