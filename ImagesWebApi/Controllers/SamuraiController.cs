using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace ImagesWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SamuraiController : Controller
    {
        private readonly BusinessLogicData _businessLogicData;

        public SamuraiController(BusinessLogicData businessLogicData)
        {
            _businessLogicData = businessLogicData;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Samurai>>> GetSamurais()
        {
           var samurais = await _businessLogicData.GetSamurais();
           return Ok(samurais);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Samurai>> GetSamurai(int id)
        {
            var samurai = await _businessLogicData.GetSamurai(id);

            if (samurai == null)
            {
                return NotFound();
            }

            return samurai;
        }
    
    }
}