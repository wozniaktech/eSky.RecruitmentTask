namespace eSky.RecruitmentTask.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
