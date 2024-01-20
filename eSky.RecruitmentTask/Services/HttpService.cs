namespace eSky.RecruitmentTask.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
  
        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentOutOfRangeException("Url can't be null or empty", nameof(url));

            var httpClient = _httpClientFactory.CreateClient();
            try
            {
                var response = await httpClient.GetAsync(url);
                return response;
            }
            catch(Exception ex)
            {
                throw new Exception($"Can't retrieve data from server. {ex.Message}");
            }
        }
    }
}
