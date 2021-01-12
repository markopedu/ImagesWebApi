using System;

namespace ImagesWebApi.Services.Cache
{
    public static class CacheKeys
    {
        public static readonly string CacheKeySamuraiList = "samurai.list";
        public static string CacheKeySamurai(int id) => $"samurai.{id}";
    }
}