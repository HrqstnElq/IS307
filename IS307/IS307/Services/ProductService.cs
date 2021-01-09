using IS307.Models;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS307.Services
{
    public class ProductService
    {
        public List<ProductModel> GetProductInCategory(string Category)
        {
            var response = Singleton.HttpClient.GetStringAsync($"/product/?category={Category}").Result;
            var result = JsonConvert.DeserializeObject<List<ProductModel>>(response);
            return result;
        }

        public List<ProductModel> GetTopProduct()
        {
            var response = Singleton.HttpClient.GetStringAsync("/product").Result;
            var result = JsonConvert.DeserializeObject<List<ProductModel>>(response);
            return result.Take(15).ToList();
        }

        public bool IsFavoriteProduct(string ProductId, string token)
        {
            Singleton.HttpClient.DefaultRequestHeaders.Add("x-auth-token", token);
            var respone = Singleton.HttpClient.GetAsync($"/product/checkFavorite/{ProductId}").Result;
            Singleton.HttpClient.DefaultRequestHeaders.Remove("x-auth-token");
            
            if (respone.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }
        public void FavoriteProduct(string ProductId, string token)
        {
            Singleton.HttpClient.DefaultRequestHeaders.Add("x-auth-token", token);
            Singleton.HttpClient.GetAsync($"/user/favorite/{ProductId}");
            Singleton.HttpClient.DefaultRequestHeaders.Remove("x-auth-token");
        }

        public async Task UnFavoriteProduct(string ProductId, string token)
        {
            Singleton.HttpClient.DefaultRequestHeaders.Add("x-auth-token", token);
            await Singleton.HttpClient.DeleteAsync($"/user/favorite/{ProductId}");
            Singleton.HttpClient.DefaultRequestHeaders.Remove("x-auth-token");
        }

        public List<ProductModel> GetUserFavoriteProduct(string token)
        {
            Singleton.HttpClient.DefaultRequestHeaders.Add("x-auth-token", token);
            var response = Singleton.HttpClient.GetStringAsync("/user/favorite").Result;
            var result = JsonConvert.DeserializeObject<List<ProductModel>>(response);
            Singleton.HttpClient.DefaultRequestHeaders.Remove("x-auth-token");
            return result;
        }
    }
}
