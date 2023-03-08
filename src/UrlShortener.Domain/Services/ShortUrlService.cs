using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Entities;
using UrlShortener.Data.Repository.Abstraction;
using UrlShortener.Domain.Abstraction;
using static System.Net.WebRequestMethods;

namespace UrlShortener.Domain.Services
{
    public class ShortUrlService :IShortUrlService
    {
        private readonly IShortUrlRepository _repo;
        private readonly ILogger<IShortUrlService> _logger;
        private readonly IMemoryCache _cache;

        public ShortUrlService(IShortUrlRepository repo, ILogger<IShortUrlService> logger, IMemoryCache cache)
        {
            _repo = repo;
            _logger = logger;
            _cache = cache;
        }

        public ShortUrlService(IShortUrlRepository repo)
        {
            _repo = repo;
        }

        public async Task<ShortUrl> GenerateShortUrl(string url)
        {
            if (_cache.TryGetValue(url, out string cachedAlias))
            {
                var cachedShortUrl = _repo.GetByAlias(cachedAlias).Result;
                _logger.LogInformation($"Requested url: {url} fetched from memory cache");

                return cachedShortUrl;
            }
            string alias = await GenerateAlias();

            var shortUrl = new ShortUrl
            {
                Alias = alias,
                Url = url,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.Add(shortUrl);
            _logger.LogInformation($"New entry: {shortUrl.Alias} created in database");
            _cache.Set(url, alias);
            _cache.Set(alias, url);
            _logger.LogInformation($"Method GenerateShortUrl: new entry: {shortUrl.Alias} stored in memory cache");

            return shortUrl;
        }

        public async Task<List<ShortUrl>> GetAllShortUrls()
        {
            return await _repo.GetAll();
        }

        public async Task<string> GetUrlByAlias(string alias)
        {
            if (_cache.TryGetValue(alias, out string url))
            {
                _logger.LogInformation($"Requested url: {url} fetched from memory cache");
                return url;
            }

            var shortUrl = await _repo.GetByAlias(alias);

            if (shortUrl == null)
            {
                return null;
            }

            _cache.Set(alias, shortUrl.Url);

            _logger.LogInformation($"Method GetAllShortUrls: new entry: {shortUrl.Alias} stored in memory cache");

            return shortUrl.Url;
        }

        private async Task<string> GenerateAlias()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();

            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}