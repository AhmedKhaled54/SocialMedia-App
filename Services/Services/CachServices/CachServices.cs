using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Services.CachServices
{
    public class CachServices : ICachServices
    {
        private readonly IDatabase _database;
        public CachServices(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<string> GetResponse(string key)
        {
            var response = await _database.StringGetAsync(key);
            if (response.IsNullOrEmpty)
                return null;

            return response;
        }



        public async Task SetResponse(string key, object response, TimeSpan timelive)
        {
            if (response is null)
                return;

            var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serailize = JsonSerializer.Serialize(response, option);
            await _database.StringSetAsync(key, serailize, timelive);

        }

        public async Task RemoveResponse(string key)
        {
            if (!string.IsNullOrEmpty(key))
                await _database.KeyDeleteAsync(key);

            return;
        }

        public async Task<T?> GetResponseGeneric<T>(string key)//=>to used in post 
        {
            var response = await _database.StringGetAsync(key);
            if (response.IsNullOrEmpty)
                return default;
            var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            return JsonSerializer.Deserialize<T>(response,option);

        }

        public async Task IncrementKey(string key) // using chah version 
            =>await _database.StringIncrementAsync(key);
    }
}
