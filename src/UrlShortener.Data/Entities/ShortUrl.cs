using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace UrlShortener.Data.Entities
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Url { get; set; }

        public string UrlWithAlias
        {
            get { return "https://happy.io/" + Alias; }
        }

        public DateTime CreatedAt { get; set; }
    }
}