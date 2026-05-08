using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Story.Command.Models
{
    public  class DeleteStoryCommand:IRequest<Response<string>>
    {
        public int StoryId { get; set; }
    }
}
