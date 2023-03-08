using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Context;
using UrlShortener.Data.Entities;
using UrlShortener.Data.Repository.Abstraction;

namespace UrlShortener.Data.Repository
{
    public class ShortUrlRepository :IShortUrlRepository
    {
        private readonly ShortUrlDbContext _context;
        private readonly ILogger<ShortUrlRepository> _logger;

        public ShortUrlRepository(ShortUrlDbContext context, ILogger<ShortUrlRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ShortUrl> Add(ShortUrl shortUrl)
        {
            try
            {
                _context.shortUrls.Add(shortUrl);
                _context.SaveChanges();
                return shortUrl;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error on creating short url in database.");
                throw;
            }
        }

        public async Task<ShortUrl> GetByAlias(string alias)
        {
            try
            {
                var shortUrl = await _context.shortUrls.FirstOrDefaultAsync(s => s.Alias == alias);
                if (shortUrl == null)
                    return null;

                return shortUrl;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting url by alias from database");
                throw;
            }
        }     
        public async Task<ShortUrl> GetByUrl(string url)
        {
            try
            {
                var shortUrl = await _context.shortUrls.FirstOrDefaultAsync(s => s.Url == url);
                if (shortUrl == null)
                    return null;

                return shortUrl;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting shorten url by url from database");
                throw;
            }
        }

        public async Task<List<ShortUrl>> GetAll()
        {
            try
            {
                return await _context.shortUrls.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error on getting all records from database");
                throw;
            }
        }
    }
}