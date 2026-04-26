using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.OtpService
{
    public  interface IOtpServices
    {
        Task SaveOtp(string email, string otp);
        Task <string>GetOtp(string email);
        Task DeleteOtp(string email);
    }
}
