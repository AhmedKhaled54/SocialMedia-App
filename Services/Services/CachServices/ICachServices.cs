using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CachServices
{
    public  interface ICachServices
    {
        Task<T?>GetResponseGeneric<T>(string key);

        Task<string> GetResponse(string key);
        Task SetResponse(string key, object response, TimeSpan timelive);
        Task RemoveResponse(string key);
        Task IncrementKey(string key);
    }
}
