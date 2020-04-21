using System;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace azure_logic_app_http_request
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            IHttpClientFactory httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            HttpClient client = httpClientFactory.CreateClient();

            string url = "[YOUR LOGIC APP URL]";

            Console.Write("Your phone: ");
            string phone = Console.ReadLine();

            string code = CreateCode();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent("{ 'code': '" + code + "', 'phone': '" + phone + "'}", Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.SendAsync(request).Result;

            Console.WriteLine(response.StatusCode);
            Console.ReadKey();
        }

        static string CreateCode()
        {
            Random rnd = new Random();
            string code = string.Empty;

            for (int i = 0; i < 6; i++)
                code += rnd.Next(0, 9).ToString();

            return code;
        }
    }
}
