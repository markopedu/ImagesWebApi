using System.Collections.Generic;
using System.Threading.Tasks;
using ImagesWebApi.Models;

namespace ImagesWebApi.Services
{
    public interface ICarouselImageService
    {
       Task<IEnumerable<CarouselImage>> GetImages();
    }
}