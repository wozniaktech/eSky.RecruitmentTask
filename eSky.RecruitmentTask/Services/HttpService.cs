using eSky.RecruitmentTask.Helper;

namespace eSky.RecruitmentTask.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public HttpService(IHttpClientFactory httpClientFactory, 
            ILogger<HttpService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                _logger.LogError(ErrorMessages.URL_CANT_BE_NULL_OR_EMPTY);
                throw new ArgumentOutOfRangeException(ErrorMessages.URL_CANT_BE_NULL_OR_EMPTY, nameof(url));
            }
                
            var httpClient = _httpClientFactory.CreateClient();
            try
            {
                var response = await httpClient.GetAsync(url);
                return response;
            }
            catch(Exception)
            {
                _logger.LogError(ErrorMessages.CANT_RETREIVE_DATA_FROM_SERVER);
                throw new Exception(ErrorMessages.CANT_RETREIVE_DATA_FROM_SERVER);
            }
        }
    }
}
