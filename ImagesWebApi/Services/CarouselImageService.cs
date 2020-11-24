using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private string GetFilePath(string fileName)
        {
            return Path.Combine(_accessor.GetUrl(), "StaticFiles", fileName);
        }
        
        public IEnumerable<CarouselImage> GetImages()
        {
            var ciList = new List<CarouselImage>
            {
                new CarouselImage
                {
                    ImgUrl = GetFilePath("nature1.jpg"),
                    Description= "Nature Sea 1",
                    Attribution= "John Doe"
                },
                new CarouselImage
                {
                    ImgUrl = GetFilePath("nature2.jpg"),
                    Description= "Nature Sea 2",
                    Attribution= "Matti Nätti"
                },
                new CarouselImage
                {
                    ImgUrl = GetFilePath("nature3.jpg"),
                    Description= "Nature 3",
                    Attribution= "Kalle Anka"
                },
            };


            return ciList.AsEnumerable();
        }
        
    }
}