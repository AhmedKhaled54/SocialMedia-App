using Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Story.Command.Models
{
    public  class AddStoryCommand:IRequest<Response<string>>
    {
        public List<IFormFile> Files { get; set; }
    }
}
