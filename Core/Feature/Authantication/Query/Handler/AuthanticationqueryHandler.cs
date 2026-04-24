using Core.Feature.Authantication.Query.Models;
using MediatR;
using Services.Services.AuthanticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.Query.Handler
{
    public class AuthanticationqueryHandler : IRequestHandler<ConfirmEmailQuery, string>
    {
        private readonly IAuthanticationServices services;

        public AuthanticationqueryHandler(IAuthanticationServices services)
        {
            this.services = services;
        }
        public async Task<string> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var Aud = await services.ConfirmEmail(request.userid, request.Code);
            if (!Aud.Success)
                return Aud.Message;

            return Aud.Message;
           
        }
    }
}
