using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawler
{
    public interface ICrawler
    {
        public IEnumerable<HttpResponseMessage> CrawlUrisSynchronously(IEnumerable<string> uris);
        public Task<IEnumerable<HttpResponseMessage>> CrawlUrisAsync(IEnumerable<string> uris);
        public Task<HttpResponseMessage> CrawlUriAsync(string uri);
    }
}
