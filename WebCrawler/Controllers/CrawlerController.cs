using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawler.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CrawlerController : ControllerBase
    {
        private readonly ILogger<CrawlerController> _logger;
        private readonly ICrawler _crawler;

        public CrawlerController(ILogger<CrawlerController> logger, ICrawler crawler)
        {
            _logger = logger;
            _crawler = crawler;
        }

        [HttpGet]
        public IEnumerable<string> CrawlSynchronously(int count = 10)
        {
            IEnumerable<string> uris = BuildUris(count);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            IEnumerable<HttpResponseMessage> resp = _crawler.CrawlUrisSync(uris);
            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            return new string[] { "elapsedMs:", elapsedMs.ToString(), "uri count:", count.ToString() };
        }

        [HttpGet]
        public IEnumerable<string> CrawlAsynchronously(int count = 10) // don't abbreviate to CrawlAsync. Ending a method with "Async" does something special and ruins the Web Api routing!
        {
            IEnumerable<string> uris = BuildUris(count);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            IEnumerable<HttpResponseMessage> resp = _crawler.CrawlUrisAsync(uris).Result; // is this going to freeze some thread in a bad way and mess up the server???
            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            return new string[] { "elapsedMs:", elapsedMs.ToString(), "uri count:", count.ToString() };
        }

        private IEnumerable<string> BuildUris(int count)
        {
            IList<string> uris = new List<string>();

            for (int i = 0; i < count / 3; i++)
            {
                uris.Add("https://google.com/");
                uris.Add("https://yahoo.com/");
                uris.Add("https://api.github.com/repos/aspnet/AspNetCore.Docs/branches");
            }

            return uris;
        }
    }
}
