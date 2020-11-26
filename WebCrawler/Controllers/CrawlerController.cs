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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CrawlerController> _logger;
        private readonly ICrawler _crawler;

        public CrawlerController(ILogger<CrawlerController> logger, ICrawler crawler)
        {
            _logger = logger;
            _crawler = crawler;
        }

        [HttpGet]
        public IEnumerable<string> CrawlSynchronously(int length = 10)
        {
            IList<string> uris = new List<string>();

            for (int i = 0; i < length / 3; i++)
            {
                uris.Add("https://google.com/");
                uris.Add("https://yahoo.com/");
                uris.Add("https://api.github.com/repos/aspnet/AspNetCore.Docs/branches");
            }

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            IEnumerable<HttpResponseMessage> resp = _crawler.CrawlUrisSync(uris);


            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            return new string[] { "elapsedMs", elapsedMs.ToString() };
            //return responseMessage;
        }

        [HttpGet]
        public IEnumerable<string> CrawlAsynchronously(int length = 10) // don't abbreviate to CrawlAsync. Ending a method with "Async" does something special and ruins the Web Api routing!
        {
            IList<string> uris = new List<string>();

            for (int i = 0; i < length / 3; i++)
            {
                uris.Add("https://google.com/");
                uris.Add("https://yahoo.com/");
                uris.Add("https://api.github.com/repos/aspnet/AspNetCore.Docs/branches");
            }

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            IEnumerable<HttpResponseMessage> resp = _crawler.CrawlUrisAsync(uris).Result; // is this going to freeze some thread in a bad way and mess up the server???


            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            return new string[] { "elapsedMs", elapsedMs.ToString() };
        }
    }
}
