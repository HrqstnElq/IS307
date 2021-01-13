using IS307.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IS307.Services
{
    public class AccountService
    {
        public async Task<string> LoginService(LoginModel loginModel)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json"))
            {
                //Singleton.HttpClient.DefaultRequestHeaders.Add("x-auth-token", "1");
                var response = await Singleton.HttpClient.PostAsync("/user/login", content);
                if (response.StatusCode == HttpStatusCode.OK)
                    return JsonConvert.DeserializeObject<LoginResultModel>(await response.Content.ReadAsStringAsync()).token;
                else
                    return null;
            }
        }

        /// <param name="registerModel"></param>
        /// <returns>
        ///  0 : Success
        ///  1 : Username already exists
        ///  -1 : Bad request
        /// </returns>
        public async Task<int> RegisterService(RegisterModel registerModel)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json"))
            {
                //Singleton.HttpClient.DefaultRequestHeaders.Add("x-auth-token", "1");
                var response = await Singleton.HttpClient.PostAsync("/user/register", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return 0;
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}