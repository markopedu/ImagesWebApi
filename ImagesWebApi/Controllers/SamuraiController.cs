using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImagesWebApi.Models.Dto;
using ImagesWebApi.Services.Cache;
using Microsoft.AspNetCore.Mvc;
using SamuraiApp.Domain;

namespace ImagesWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SamuraiController : Controller
    {
        private const string CacheKeySamuraiList = "samurai.list";

        private readonly BusinessLogicData _businessLogicData;
        private readonly SamuraiCacheService _samuraiCacheService;
        private readonly SamuraiCacheListService _samuraiCacheListService;

        public SamuraiController(BusinessLogicData businessLogicData, ICacheService<SamuraiDto> samuraiCacheService, ICacheService<IEnumerable<SamuraiDto>> samuraiCacheListService)
        {
            _businessLogicData = businessLogicData;
            _samuraiCacheService = samuraiCacheService as SamuraiCacheService;
            _samuraiCacheListService = samuraiCacheListService as SamuraiCacheListService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Samurai>>> GetSamurais()
        {
           var samuraiList = await _samuraiCacheListService.GetCacheValueAsync(CacheKeySamuraiList);

           if (samuraiList != null) return Ok(samuraiList);
           
           var samurais = await _businessLogicData.GetSamurais();

           var samuraiListDtos = samurais.Select(x => new SamuraiDto
           {
               Name = x.Name
           });

           await _samuraiCacheListService.SetCacheValueAsync(CacheKeySamuraiList, samuraiListDtos);

           return Ok(samuraiListDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Samurai>> GetSamurai(int id)
        {
            var key = $"samurai.{id}";

            var cachedSamurai = await _samuraiCacheService.GetCacheValueAsync(key);

            if (cachedSamurai != null) return Ok(cachedSamurai);
            
            var samurai = await _businessLogicData.GetSamurai(id);

            if (samurai == null) return NotFound();

            var samDto = new SamuraiDto
            {
                Name = samurai.Name
            };
  
            await _samuraiCacheService.SetCacheValueAsync(key, samDto);
            
            return Ok(samDto);
        }
    
    }
}