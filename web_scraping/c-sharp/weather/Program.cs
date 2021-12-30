using System;
using System.Linq;
using HtmlAgilityPack;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace weather
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://forecast.weather.gov/MapClick.php?lat=43.1&lon=-77.5";
            
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            //client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:95.0) Gecko/20100101 Firefox/95.0");
            HttpResponseMessage response = await client.GetAsync(url);
            
            //was the response successful?
            if (response.IsSuccessStatusCode)
            {
                string html = await response.Content.ReadAsStringAsync();
                HtmlDocument parser = new HtmlDocument();
                parser.LoadHtml(html);
                
                string location = parser.DocumentNode.Descendants("h2")
                    .First(node => node.GetClasses().Contains("panel-title")).InnerText;

                string temp = parser.DocumentNode.Descendants("p")
                    .First(node => node.HasClass("myforecast-current-lrg")).InnerText;


                Console.WriteLine($"Current temperature in {location} is {WebUtility.HtmlDecode(temp)}");
            }
            //an error occured with the response
            else
            {
                Console.WriteLine($"The requested url returned {response.StatusCode}");
            }
        }
        
    }
}
