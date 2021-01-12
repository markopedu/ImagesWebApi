using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace ImagesWebApi
{
    public class BusinessLogicData
    {
        private readonly SamuraiContext _context;

        public BusinessLogicData(SamuraiContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Samurai>> GetSamurais()
        {
            return await _context.Samurais
                .Take(100)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Samurai> GetSamurai(int id)
        {
           return await _context.Samurais.FindAsync(id);
        }

        public async Task CreateNewSamurai(Samurai samurai)
        {
            await _context.Samurais.AddAsync(samurai);
            await _context.SaveChangesAsync();
        }
    }
}