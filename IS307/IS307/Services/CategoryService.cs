using IS307.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IS307.Services
{
    public class CategoryService
    {
        public async Task<List<CategoryModel>> GetAllCategory()
        {
            var response = await Singleton.HttpClient.GetStringAsync("/product/categories");
            var result = JsonConvert.DeserializeObject<List<CategoryModel>>(response);
            return result;
        }
    }
}
