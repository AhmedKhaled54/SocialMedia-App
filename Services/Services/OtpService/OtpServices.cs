using Services.Services.CachServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.OtpService
{
    public class OtpServices : IOtpServices
    {
        private readonly ICachServices _cach;

        public OtpServices(ICachServices cach)
        {
            _cach = cach;
        }
     
        public async Task<string> GetOtp(string email)
           => await _cach.GetResponse(email);

        public async Task SaveOtp(string email, string otp)
        =>  await _cach.SetResponse(email, otp, TimeSpan.FromMinutes(5));
       
        public async  Task   DeleteOtp(string email)
             => await _cach.RemoveResponse(email);

       
    }
}
