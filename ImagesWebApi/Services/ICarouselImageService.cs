using System.Collections.Generic;
using ImagesWebApi.Models;

namespace ImagesWebApi.Services
{
    public interface ICarouselImageService
    {
        IEnumerable<CarouselImage> GetImages();
    }
}