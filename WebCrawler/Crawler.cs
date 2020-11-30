﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class Crawler : ICrawler
    {
        private readonly IHttpClientFactory _clientFactory;
        public Crawler(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IEnumerable<HttpResponseMessage> CrawlUrisSync(IEnumerable<string> uris)
        {
            IList<HttpResponseMessage> result = new List<HttpResponseMessage>();

            foreach(var uri in uris)
            {
                HttpResponseMessage response = CrawlUriAsync(uri).Result; // this blocks the current thread
                result.Add(response);
            }

            return result;
        }

        public async Task<IEnumerable<HttpResponseMessage>> CrawlUrisAsync(IEnumerable<string> uris)
        {
            IList<HttpResponseMessage> result = new List<HttpResponseMessage>();

            var client = _clientFactory.CreateClient();

            // We simple setup a List of Tasks
            var requests = uris.Select
            (
                uri => CrawlUriAsync(uri)
            ).ToList();

            await Task.WhenAll(requests);

            //Get the responses
            var responses = requests.Select
            (
                task => task.Result
            );

            return responses;
        }

        public async Task<HttpResponseMessage> CrawlUriAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            return response;
        }
    }
}
