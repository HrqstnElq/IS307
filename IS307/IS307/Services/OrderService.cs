using IS307.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<ViewOrderModel>> GetOrders(string token)
        {
            Singleton.HttpClient.DefaultRequestHeaders.Add("x-auth-token", token);
            var result = await Singleton.HttpClient.GetStringAsync("/order");
            var orders = JsonConvert.DeserializeObject<List<ViewOrderModel>>(result);
            Singleton.HttpClient.DefaultRequestHeaders.Remove("x-auth-token");
            return orders;
        }
    }
}