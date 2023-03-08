using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Entities;

namespace UrlShortener.Domain.Abstraction
{
    public interface IShortUrlService
    {
        Task<ShortUrl> GenerateShortUrl(string url);

        Task<string> GetUrlByAlias(string alias);

        Task<List<ShortUrl>> GetAllShortUrls();
    }
}