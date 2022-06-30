using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ValorantAPI.Clients
{
    public class ValorantNewsClient
    {
        private HttpClientHandler _handler;
        private HttpClient _client;

        public ValorantNewsClient()
        {
            _handler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip
                | System.Net.DecompressionMethods.None
            };

            _client = new HttpClient(_handler);
        }

        public async Task<List<string>> NewsParsing(string url)
        {
            List<string> result = new List<string>();

            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var html = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(html))
                {
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(html);

                    var caption = document.DocumentNode.SelectNodes(".//p[@class='copy-02 NewsCard-module--description--3sFiD']");
                    var href = document.DocumentNode.SelectNodes(".//a");

                    for (int i = 0; i < 2; i++)
                    {
                        StringBuilder stringBuilder = new StringBuilder(caption[i].InnerText);
                        stringBuilder.Replace("&quot;", "\"");
                        result.Add($"! {stringBuilder} !\n\n");

                        if (href[i].GetAttributeValue("href", null).StartsWith("https"))
                        {
                            result.Add($"{href[i].GetAttributeValue("href", null)}\n\n");
                        }
                        else
                        {
                            string newUrl = ($"https://playvalorant.com{href[i].GetAttributeValue("href", null)}");

                            var newResponse = await _client.GetAsync(newUrl);
                            if (newResponse.IsSuccessStatusCode)
                            {
                                var newHtml = newResponse.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(newHtml))
                                {
                                    document = new HtmlDocument();
                                    document.LoadHtml(newHtml);

                                    var news = document.DocumentNode.SelectNodes(".//div[@class='copy-02 NewsArticleContent-module--articleTextContent--2yATc']");

                                    for (int j = 0; j < news.Count; j++)
                                    {
                                        StringBuilder newStringBuilder = new StringBuilder(news[j].InnerText);
                                        newStringBuilder.Replace("&quot;", "\"");
                                        result.Add($"{newStringBuilder}\n\n");
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            return result;
        }

        //public async Task<List<string>> NewsParsing(string url)
        //{
        //    List<string> result = new List<string>();

        //    var response = await _client.GetAsync(url);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var html = response.Content.ReadAsStringAsync().Result;
        //        if (!string.IsNullOrEmpty(html))
        //        {
        //            HtmlDocument document = new HtmlDocument();
        //            document.LoadHtml(html);

        //            var res = document.DocumentNode.SelectNodes(".//div[@class='copy-02 NewsArticleContent-module--articleTextContent--2yATc']/ul");

        //            foreach (var item in res)
        //            {
        //                StringBuilder sb = new StringBuilder(item.InnerText);
        //                sb.Replace("&quot;", "\"");

        //                result.Add(sb.ToString());
        //            }
        //        }
        //    }

        //    return result;
        //}

        public async Task<List<string>> TournamentsParsing(string url)
        {
            List<string> result = new List<string>();

            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var html = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(html))
                {
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(html);

                    var tournamentName = document.DocumentNode.SelectNodes(".//div[@class='events-container-col'][1]//div[@class='event-item-title']");
                    var prizePool = document.DocumentNode.SelectNodes(".//div[@class='events-container-col'][1]//div[@class='event-item-desc-item mod-prize']");
                    var dates = document.DocumentNode.SelectNodes(".//div[@class='events-container-col'][1]//div[@class='event-item-desc-item mod-dates']");
                    var href = document.DocumentNode.SelectNodes(".//div[@class='events-container-col'][1]//a[@class='wf-card mod-flex event-item']");

                    for (int i = 0; i < 4; i++)
                    {
                        StringBuilder sb = new StringBuilder(tournamentName[i].InnerText);
                        sb.Replace("\t", "");
                        result.Add($"{sb}\n");

                        sb = new StringBuilder(prizePool[i].InnerText);
                        sb.Replace("\t", "");
                        result.Add(sb.ToString());

                        sb = new StringBuilder(dates[i].InnerText);
                        sb.Replace("\t", "");
                        result.Add(sb.ToString());

                        result.Add($"https://www.vlr.gg{href[i].GetAttributeValue("href", null)}\n");
                    }
                }
            }

            return result;
        }
    }
}
