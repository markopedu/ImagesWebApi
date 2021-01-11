using System.Threading.Tasks;
using ImagesWebApi.Services.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImagesWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly ICacheService<string> _cacheService;

        public TestController(ICacheService<string> cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get(string key)
        {
           var value = await _cacheService.GetCacheValueAsync(key);

           if (string.IsNullOrEmpty(value))
           {
               return NotFound();
           }

           return Ok(value);
        }


        [HttpPost]
        public async Task Post(IFormCollection fc)
        {
            var key = fc["key"];
            var value = fc["value"];
            
            await _cacheService.SetCacheValueAsync(key, () => value);
        }
    }
}