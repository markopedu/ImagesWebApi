using System;
using System.Threading.Tasks;

namespace ImagesWebApi.Services.Cache
{
    public interface ICacheService<T>
    {
        Task<T> GetCacheValueAsync(string key);

        Task SetCacheValueAsync(string key, T value);
        
        Task<bool> RemoveKey(string key);
    }
}