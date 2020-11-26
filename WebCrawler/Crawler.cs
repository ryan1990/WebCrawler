using System;
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

        //public async Task<IEnumerable<string>> CrawlUrisAsync(IEnumerable<string> uris)
        //{
        //    // make parralel

        //    string uri = "https://api.github.com/repos/aspnet/AspNetCore.Docs/branches";
        //    await CrawlUriAsync(uri);
        //}

        public async Task<HttpResponseMessage> CrawlUriAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            return response;

            //if (response.IsSuccessStatusCode)
            //{
            //    using var responseStream = await response.Content.ReadAsStreamAsync();
            //    Branches = await JsonSerializer.DeserializeAsync
            //        <IEnumerable<GitHubBranch>>(responseStream);
            //}
            //else
            //{
            //    GetBranchesError = true;
            //    Branches = Array.Empty<GitHubBranch>();
            //}
        }
    }
}
