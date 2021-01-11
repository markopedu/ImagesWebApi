using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImagesWebApi.Models.Dto;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ImagesWebApi.Services.Cache
{
    public class SamuraiCacheListService : ICacheService<IEnumerable<SamuraiDto>>
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public SamuraiCacheListService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<IEnumerable<SamuraiDto>> GetCacheValueAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var r = await db.StringGetAsync(key);
            return !r.IsNull ? JsonConvert.DeserializeObject<IEnumerable<SamuraiDto>>(r) : null;
        }

        public async Task SetCacheValueAsync(string key, IEnumerable<SamuraiDto> value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }

        public async Task<bool> RemoveKey(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.KeyDeleteAsync(key);
        }
    }
}