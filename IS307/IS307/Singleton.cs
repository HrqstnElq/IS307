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
                if(httpClient == null)
                {
                    httpClient = new HttpClient();
                    httpClient.BaseAddress = new System.Uri("http://172.20.7.8:3000");
                }

                return httpClient;
            }
        }


    }
}
