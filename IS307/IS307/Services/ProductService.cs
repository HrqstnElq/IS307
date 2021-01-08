using IS307.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
