using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrlShortener.Data.Context;
using UrlShortener.Domain.Abstraction;
using UrlShortener.WebApplication.Models;

namespace UrlShortener.WebApplication.Controllers
{
    public class HomeController :Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShortUrlService _shortUrlService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IShortUrlService service, IMapper mapper)
        {
            _logger = logger;
            _shortUrlService = service;
            _mapper = mapper;
        }

        public IMapper Mapper { get; }

        public IActionResult Index()
        {
            var shortUrlList = _shortUrlService.GetAllShortUrls().Result;
            var urlListDto = _mapper.Map<List<ShortUrlResponse>>(shortUrlList);
            return View(urlListDto);
        }

        public IActionResult TechTaskDetails()
        {
            return View("TechTaskDetails");
        }
    }
}