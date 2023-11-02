using eSky.RecruitmentTask.Models;

namespace eSky.RecruitmentTask.Services
{
    public interface IAuthorService
    {
        Task<List<string>> GetAuthors(int numberOfAuthors);
        Task<List<Author>> GetPoemsByAuthor(List<string> authors);
    }
}