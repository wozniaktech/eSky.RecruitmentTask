using eSky.RecruitmentTask.Helper;
using eSky.RecruitmentTask.Models;
using Newtonsoft.Json;

namespace eSky.RecruitmentTask.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthorService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //HttpClient client = new HttpClient();
        const string authorsPath = "https://poetrydb.org/authors";
        const string poemsPath = "https://poetrydb.org/author/";


        public async Task<List<Author>> GetPoemsByAuthor(List<string> authors)
        {
            var poems = new List<Poem>();
            var authorsList = new List<Author>();

            if ((authors != null) && (authors.Any()))
            {
                foreach (var author in authors)
                {
                    //var response = await client.GetAsync(poemsPath + author);
                    var httpClient = _httpClientFactory.CreateClient();
                    var response = await httpClient.GetAsync(poemsPath + author);

                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync().Result;
                        poems = JsonConvert.DeserializeObject<List<Poem>>(res);
                    }
                    // Get authors with poems only.
                    if ((poems != null) && (poems.Any()))
                    {
                        var newAuthor = new Author()
                        {
                            Poems = new List<string>()
                        };

                        foreach (var peom in poems)
                        {
                            if (String.IsNullOrEmpty(newAuthor.Name))
                            {
                                newAuthor.Name = peom.Author;
                            }

                            newAuthor.Poems.Add(peom.Title);
                        }
                        authorsList.Add(newAuthor);
                    }
                }
            }
            return authorsList;
        }

        public async Task<List<string>> GetAuthors(int numberOfAuthors)
        {
            if (numberOfAuthors <= 0)
                throw new ArgumentOutOfRangeException("Number of authors should be bigger then zero!", nameof(numberOfAuthors));

            AuthorsList? authors = new AuthorsList();
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(authorsPath);
            List<string> result = new List<string>();

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                authors = JsonConvert.DeserializeObject<AuthorsList>(res);
                result = authors.Authors.GetRandomAuthors(numberOfAuthors);
            }
            return result;
        }
    }
}