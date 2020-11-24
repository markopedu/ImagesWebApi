using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImagesWebApi.Extensions;
using ImagesWebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ImagesWebApi.Services
{
    public class CarouselImageService : ICarouselImageService
    {
        private readonly IHttpContextAccessor _accessor;

        public CarouselImageService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        
        public async Task<IEnumerable<CarouselImage>> GetImages()
        {
            var ciList = new List<CarouselImage>
            {
                new CarouselImage
                {
                    ImgUrl = _accessor.GetFilePath("nature1.jpg"),
                    Description= "Nature Sea 1",
                    Attribution= "John Doe"
                },
                new CarouselImage
                {
                    ImgUrl = _accessor.GetFilePath("nature2.jpg"),
                    Description= "Nature Sea 2",
                    Attribution= "Matti Nätti"
                },
                new CarouselImage
                {
                    ImgUrl = _accessor.GetFilePath("nature3.jpg"),
                    Description= "Nature 3",
                    Attribution= "Kalle Anka"
                },
                new CarouselImage
                {
                    ImgUrl = _accessor.GetFilePath("helsinki.jpg"),
                    Description= "Helsinki",
                    Attribution= "Sanna Mattinen"
                },
                new CarouselImage
                {
                    ImgUrl = _accessor.GetFilePath("copenhagen.jpg"),
                    Description= "Copenhagen",
                    Attribution= "Mads Amundsen"
                },
                new CarouselImage
                {
                    ImgUrl = _accessor.GetFilePath("mediterrian.jpg"),
                    Description= "Mediterrian",
                    Attribution= "Paolo Pavarotti"
                },
            };
            
            return await Task.Run(() => ciList);
        }
        
    }
}