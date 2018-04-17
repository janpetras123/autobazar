using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OdvezAuto.Server
{
    public class HttpManager
    {
        public static async Task<T> HttpGet<T>(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                string json = await client.GetStringAsync(url);

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (HttpRequestException e)
            {
                throw new WebException(e.Message, e);
            }
        }

        public static async Task HttpPost<T>(T data, string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                string json = JsonConvert.SerializeObject(data);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                await client.PostAsync(url, content);
            }
            catch (HttpRequestException e)
            {
                throw new WebException(e.Message, e);
            }
        }

        public static async Task HttpDelete(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                await client.DeleteAsync(url);
            }
            catch (HttpRequestException e)
            {
                throw new WebException(e.Message, e);
            }
        }

        public static async Task HttpPut<T>(T data, string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                string json = JsonConvert.SerializeObject(data);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                await client.PutAsync(url, content);
            }
            catch (HttpRequestException e)
            {
                throw new WebException(e.Message, e);
            }
        }

        public static string GetHostAddress()
        {
            return "http://192.168.100.251:8080";
        }
    }
}