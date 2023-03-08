using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace UrlShortener.Data.Context
{
    public class ShortUrlResponse
    {

        public string Alias { get; set; }
        public string Url { get; set; }
        public string UrlWithAlias { get; set; }
    }
}