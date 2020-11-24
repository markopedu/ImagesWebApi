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
        
    }
}