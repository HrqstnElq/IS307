using IS307.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace IS307.Services
{
    public class OrderService
    {
        public void PostOrder(string token, OrderModel order)
        {
            var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
            Singleton.HttpClient.DefaultRequestHeaders.Add("x-auth-token", token);
            Singleton.HttpClient.PostAsync("/order", content);
            Singleton.HttpClient.DefaultRequestHeaders.Remove("x-auth-token");
        }
    }
}