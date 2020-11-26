using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawler
{
    public interface ICrawler
    {
        Task<HttpResponseMessage> CrawlUriAsync(string uri);
    }
}
