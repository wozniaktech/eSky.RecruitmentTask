using eSky.RecruitmentTask.Models;

namespace eSky.RecruitmentTask.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<string>> GetAuthors(int numberOfAuthors);
        //Task<IEnumerable<Author>> GetPoemsByAuthor(IEnumerable<string> authors);
        Task<IEnumerable<Poem>> GetPoems2(IEnumerable<string> authors);
    }
}