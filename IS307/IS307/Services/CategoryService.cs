using IS307.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IS307.Services
{
    public class CategoryService
    {
        public List<CategoryModel> GetAllCategory()
        {
            var response = Singleton.HttpClient.GetStringAsync("/product/categories").Result;
            var result = JsonConvert.DeserializeObject<List<CategoryModel>>(response);
            return result;
        }
    }
}
