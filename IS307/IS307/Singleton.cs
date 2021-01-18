using System.Net.Http;

namespace IS307
{
    //singleton pattent
    public static class Singleton
    {
        private static HttpClient httpClient;

        public static HttpClient HttpClient
        {
            get
            {
                if (httpClient == null)
                {
                    httpClient = new HttpClient();
                    httpClient.BaseAddress = new System.Uri("http://ie304-didong-api.herokuapp.com");
                    //httpClient.BaseAddress = new System.Uri("http://192.168.137.1:3000");
                }
                return httpClient;
            }
        }
    }
}