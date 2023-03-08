using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Data.Context;
using UrlShortener.Domain.Abstraction;

namespace UrlShortener.WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortUrlApiController :ControllerBase
    {
        private readonly ILogger<ShortUrlApiController> _logger;
        private readonly IMapper _mapper;
        private readonly IShortUrlService _shortUrlService;

        public ShortUrlApiController(ILogger<ShortUrlApiController> logger, IMapper mapper, IShortUrlService shortUrlService)
        {
            _logger = logger;
            _mapper = mapper;
            _shortUrlService = shortUrlService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateShortUrl([FromBody] ShortUrlRequest request)
        {
            if (!Uri.TryCreate(request.Url, UriKind.Absolute, out Uri uriResult))
            {
                return BadRequest("Invalid URL");
            }

            var shortUrl = _shortUrlService.GenerateShortUrl(request.Url).Result;
            var response = _mapper.Map<ShortUrlResponse>(shortUrl);

            return Ok(response);
        }

        [HttpGet("{alias}")]
        public async Task<IActionResult> Redirection([FromRoute] string alias)
        {
            if (String.IsNullOrEmpty(alias))
                return NotFound();

            var url = _shortUrlService.GetUrlByAlias(alias).Result;
            if (url == null)

                return NotFound();

            return RedirectPermanent(url);
        }
    }
}