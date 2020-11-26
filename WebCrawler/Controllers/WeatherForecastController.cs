using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICrawler _crawler;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICrawler crawler)
        {
            _logger = logger;
            _crawler = crawler;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            string uri = "https://api.github.com/repos/aspnet/AspNetCore.Docs/branches";
            Task<HttpResponseMessage> respTask = _crawler.CrawlUriAsync(uri);

            //respTask.RunSynchronously();
            HttpResponseMessage responseMessage = respTask.Result;

            return new List<WeatherForecast>();
            //return responseMessage;


            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}
