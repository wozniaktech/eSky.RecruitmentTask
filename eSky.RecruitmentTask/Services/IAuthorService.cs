using eSky.RecruitmentTask.Models;

namespace eSky.RecruitmentTask.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<string>> GetAuthors(int numberOfAuthors);
        Task<IEnumerable<Poem>> GetPoems(IEnumerable<string> authors);
        Task <IEnumerable<Author>> GetPoemsByAuthor(IEnumerable<Poem>? poems, IEnumerable<string> authors);
    }
}