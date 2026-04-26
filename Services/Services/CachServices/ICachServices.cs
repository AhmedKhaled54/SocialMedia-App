using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CachServices
{
    public  interface ICachServices
    {
        Task<string> GetResponse(string key);
        Task SetResponse(string key, object response, TimeSpan timelive);
        Task RemoveResponse(string key);
    }
}
