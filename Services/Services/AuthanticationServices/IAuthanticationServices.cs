using Data.Helper;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.AuthanticationServices
{
    public  interface IAuthanticationServices
    {
        Task<ResponseDto> Register(User user, string password);
        Task<AuthResult> GetToken(User user);
        Task<AuthResult> RefreshToken(string token );
        Task<ResponseDto> ConfirmEmail(int? userid, string? code);

      
    }
}
