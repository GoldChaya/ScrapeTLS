using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AngleSharp.Html.Parser;
using AngleSharp.Text;

namespace Homework_June_7.Data
{
    public static class TLSScraper
    {
        public static List<TLSNews> Scrape()
        {
            var html = GetTLSHtml();
            return ParseHtml(html);
        }

        private static string GetTLSHtml()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
                UseCookies = true
            };
            using var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("User-Agent", "TLS");

            var url = $"https://thelakewoodscoop.com/";
            var html = client.GetStringAsync(url).Result;
            return html;
        }
        private static List<TLSNews> ParseHtml(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);

            var divs = document.QuerySelectorAll(".td_module_flex.td_module_flex_5.td_module_wrap.td-animation-stack");
            var items = new List<TLSNews>();
            foreach (var div in divs)
            {
                TLSNews item = new();
                var titleElement = div.QuerySelector(".entry-title.td-module-title");
                if (titleElement != null)
                {
                    item.Title = titleElement.TextContent;
                }


                var url = div.QuerySelector("a");

                if (url != null)
                {
                    item.Url = url.Attributes["href"].Value;
                }
                var text = div.QuerySelector(".td-excerpt");
                if (text != null)
                {
                    item.Text = titleElement.TextContent;
                }

                var image = div.QuerySelector(".entry-thumb.td-thumb-css");
                if (image != null)
                {
                    item.Image = image.Attributes["data-img-url"].Value;
                }

                var comments = div.QuerySelector(".td-module-comments");
                if (comments != null)
                {
                    item.Comments = comments.TextContent;
                }

                items.Add(item);
        }

            return items;
        }
    }
}
