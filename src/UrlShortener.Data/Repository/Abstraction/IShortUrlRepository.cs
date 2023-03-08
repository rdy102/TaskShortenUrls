using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Entities;

namespace UrlShortener.Data.Repository.Abstraction
{
    public interface IShortUrlRepository
    {
        Task<ShortUrl> Add(ShortUrl shortUrl);

        Task<List<ShortUrl>> GetAll();

        Task<ShortUrl> GetByAlias(string alias);
        Task<ShortUrl> GetByUrl(string url);
    }
}