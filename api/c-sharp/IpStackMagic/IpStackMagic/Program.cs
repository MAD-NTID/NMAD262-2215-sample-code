using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IpStackMagic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "http://api.ipstack.com/check?access_key=";
            HttpClient client = new HttpClient();


            HttpResponseMessage response = await client.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                string msg = await response.Content.ReadAsStringAsync();
                dynamic info = JsonConvert.DeserializeObject<dynamic>(msg);
                Console.WriteLine(info);
                Console.WriteLine($"Hello user, you are connecting from {info.region_code}");
            } else
            {
                Console.WriteLine("No bueno!");
                Console.WriteLine(response.StatusCode);
            }
            

        }
    }
}
