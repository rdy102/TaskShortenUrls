using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Data.Context
{
    public class ShortUrlRequest
    {
        [Required]
        [Url]
        public string Url { get; set; }
    }
}