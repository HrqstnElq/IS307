using IS307.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IS307.Services
{
    public class ProductService
    {
        public async Task<List<ProductModel>> GetProductInCategory(string Category)
        {
            var response = await Singleton.HttpClient.GetStringAsync($"/product/?category={Category}");
            var result = JsonConvert.DeserializeObject<List<ProductModel>>(response);
            return result;
        }

        public async Task<List<ProductModel>> GetTopProduct()
        {
            var response = await Singleton.HttpClient.GetStringAsync("/product/top");
            var result = JsonConvert.DeserializeObject<List<ProductModel>>(response);
            return result.ToList();
        }

        public async Task<bool> IsFavoriteProduct(string ProductId, string token)
        {
            Singleton.HttpClient.DefaultRequestHeaders.Add("x-auth-token", token);
            var respone = await Singleton.HttpClient.GetAsync($"/product/checkFavorite/{ProductId}");
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

        public void UnFavoriteProduct(string ProductId, string token)
        {
            Singleton.HttpClient.DefaultRequestHeaders.Add("x-auth-token", token);
            Singleton.HttpClient.DeleteAsync($"/user/favorite/{ProductId}");
            Singleton.HttpClient.DefaultRequestHeaders.Remove("x-auth-token");
        }

        public async Task<List<ProductModel>> GetUserFavoriteProduct(string token)
        {
            Singleton.HttpClient.DefaultRequestHeaders.Add("x-auth-token", token);
            var response = await Singleton.HttpClient.GetStringAsync("/user/favorite");
            var result = JsonConvert.DeserializeObject<List<ProductModel>>(response);
            Singleton.HttpClient.DefaultRequestHeaders.Remove("x-auth-token");
            return result;
        }

        public async Task<List<ProductModel>> SearchProduct(string search)
        {
            var response = await Singleton.HttpClient.GetStringAsync($"/product/?search={search}");
            var result = JsonConvert.DeserializeObject<List<ProductModel>>(response);
            return result;
        }
    }
}