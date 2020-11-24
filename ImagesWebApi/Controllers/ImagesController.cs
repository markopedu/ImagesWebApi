using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImagesWebApi.Models;
using ImagesWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImagesWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : Controller
    {

        private readonly ILogger<ImagesController> _logger;
        private readonly ICarouselImageService _carouselImageService;

        public ImagesController(ILogger<ImagesController> logger, ICarouselImageService carouselImageService)
        {
            _logger = logger;
            _carouselImageService = carouselImageService;
        }

        [HttpGet]
        public async Task<IEnumerable<CarouselImage>> Get() => await _carouselImageService.GetImages();
    }
}