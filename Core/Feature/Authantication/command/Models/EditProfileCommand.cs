using Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Models
{
    public  class EditProfileCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; }
        public string Bio { get; set; }
        public IFormFile ?Image { get; set; }


    }
}
