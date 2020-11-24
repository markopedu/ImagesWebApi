using System.IO;
using Microsoft.AspNetCore.Http;

namespace ImagesWebApi.Extensions
{
    public static class HttpAccessorExtension
    {

        public static string GetUrl(this IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }
        
        public static string GetFilePath(this IHttpContextAccessor httpContextAccessor, string fileName)
        {
            return Path.Combine(httpContextAccessor.GetUrl(), "StaticFiles", fileName);
        }
        
    }
}