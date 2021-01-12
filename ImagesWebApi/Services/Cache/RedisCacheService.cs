using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace ImagesWebApi.Services.Cache
{
    public class RedisCacheService : ICacheService<string>
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key, value, TimeSpan.FromMinutes(1));
        }

        public async Task<bool> RemoveKey(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.KeyDeleteAsync(key);
        }
    }
}