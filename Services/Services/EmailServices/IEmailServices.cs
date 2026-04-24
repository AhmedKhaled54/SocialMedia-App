using Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.EmailServices
{
    public  interface IEmailServices
    {
        Task SendEmail(EmailDto dto);
    }
}
