
using System.Diagnostics;
using System.Net.Http.Headers;

namespace ABC {
    public class NetworkService {

        readonly IConfiguration configuration;

        public NetworkService(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<T> HttpGet<T>(String url) {

            try {
                HttpClient httpClient = new();

                httpClient.DefaultRequestHeaders.Add("channel", "web");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



                var result = await httpClient.GetAsync(url);

                if (result.IsSuccessStatusCode) {
                    T response = await result.Content.ReadFromJsonAsync<T>();
                    return response;
                }

                Debug.WriteLine($"http/GET: {url} , Error");
                return default(T);

            }
            catch (Exception ex) {
                Debug.WriteLine($"{ex.Message} \n {ex.StackTrace}");

                return default(T);
            }
        }

    }
}
