using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Context;
using UrlShortener.Data.Entities;

namespace UrlShortener.WebApplication.Controllers
{
    public class ShortUrlProfile :Profile
    {
        public ShortUrlProfile()
        {
            CreateMap<ShortUrl, ShortUrlResponse>()
                .ReverseMap();
        }
    }
}