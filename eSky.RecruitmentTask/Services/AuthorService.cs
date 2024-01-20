using eSky.RecruitmentTask.Config;
using eSky.RecruitmentTask.Helper;
using eSky.RecruitmentTask.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace eSky.RecruitmentTask.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IHttpService _httpService;
        private readonly EndpointConfig _endpoints;

        public AuthorService(IHttpService httpService, IOptionsMonitor<EndpointConfig> endpointConfig)
        {
            _httpService = httpService;
            _endpoints = endpointConfig.CurrentValue;
        }

        public IEnumerable<Author> GetP(IEnumerable<Poem>? poems, List<string> authors)
        {
            if ((poems == null) || (!poems.Any()))
                throw new ArgumentOutOfRangeException("List of poems can't by null or empty", nameof(poems));

            if (string.IsNullOrEmpty(_endpoints.Authors))
                throw new ArgumentOutOfRangeException("List of authors can't by null or empty", nameof(authors));

            var authorsList = new List<Author>();

            foreach (var author in authors) 
            {
                var newAuthor = new Author()
                {
                    Name = author,
                    Poems = new List<string>()
                };

                foreach (var peom in poems)
                {
                    if (newAuthor.Name == peom.Author )
                    { 
                        newAuthor.Poems.Add(peom.Title);
                    }

                    //if (String.IsNullOrEmpty(newAuthor.Name))
                    //{
                    //    newAuthor.Name = peom.Author;
                    //}

                    //newAuthor.Poems.Add(peom.Title);
                }
                authorsList.Add(newAuthor);
            }


           
               

               
            return authorsList;
            
        }

        public async Task<IEnumerable<Poem>> GetPoems2(IEnumerable<string> authors)
        {
            if (string.IsNullOrEmpty(_endpoints.Authors))
                throw new ArgumentOutOfRangeException("List of authors can't by null or empty", nameof(authors));

            //I used list below because I need AddRange method, which is not available in IEnumerable, ICollection nor IList
            List<Poem> poems = new List<Poem>();
            
            if (authors.Any())
            {
                var poemsTasks = authors.Select(author => _httpService.GetAsync(_endpoints.Poems + author));
                var poemsResponses = await Task.WhenAll(poemsTasks);

                if(poemsResponses.Any())
                {
                    foreach (var response in poemsResponses)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var res = await response.Content.ReadAsStringAsync();

                            if (!string.IsNullOrEmpty(res))
                            {
                                var listOfPoems = JsonConvert.DeserializeObject<List<Poem>>(res);
                                if (listOfPoems != null)
                                {
                                    poems.AddRange(listOfPoems);
                                }
                            }
                        }
                    }
                }
            }
            return poems;
         }


        //public async Task<IEnumerable<Poem>> GetPoems(IEnumerable<string> authors)
        //{
        //    if (string.IsNullOrEmpty(_endpoints.Authors))
        //        throw new ArgumentOutOfRangeException("List of authors can't by null or empty", nameof(authors));

        //    List<Poem> poems = new List<Poem>();
        //    ICollection<Task<HttpResponseMessage>> taskList = new List<Task<HttpResponseMessage>>();
         
        //    if (authors.Any())
        //    {
        //        foreach (var author in authors)
        //        {
        //            //taskList.Add("_httpService.GetAsync(_endpoints.Poets + author)");
        //            taskList.Add(_httpService.GetAsync(_endpoints.Poems + author));
        //        }
        //    }
        //    else
        //    {
        //        throw new ArgumentNullException("There are no authors", nameof(authors));
        //    }

        //    try
        //    {
        //        var responses = await Task.WhenAll(taskList);
        //        if(responses.Any())
        //        {
        //            foreach (var response in responses)
        //            {
        //                var res = await response.Content.ReadAsStringAsync();
        //                var listOfPoems = JsonConvert.DeserializeObject<List<Poem>>(res);
        //                if (listOfPoems != null)
        //                {
        //                    poems.AddRange(listOfPoems);
        //                }
        //                else
        //                {
        //                    throw new ArgumentNullException("List of poems is null or empty", nameof(listOfPoems));
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception($"Error occurred: {ex.Message}");
        //    }
        //    return poems;
        //}        
                
        //public async Task<List<Author>> GetPoemsByAuthor(List<string> authors)
        //{
        //    var poems = new List<Poem>();
        //    var authorsList = new List<Author>();

        //    //var p = GetPoems(authors).Result;
        //    //var a = GetP(p, authors);

        //    if ((authors != null) && (authors.Any()))
        //    {
        //        foreach (var author in authors)
        //        {
        //            //var response = await client.GetAsync(poemsPath + author);
        //            //var httpClient = _httpClientFactory.CreateClient();
        //            //var response = await httpClient.GetAsync(_endpoints.Poets + author);
        //            var response = await _httpService.GetAsync(_endpoints.Poems+author);

                    

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var res = response.Content.ReadAsStringAsync().Result;
        //                poems = JsonConvert.DeserializeObject<List<Poem>>(res);
        //            }
        //            // Get authors with poems only.
        //            if ((poems != null) && (poems.Any()))
        //            {
        //                var newAuthor = new Author()
        //                {
        //                    Poems = new List<string>()
        //                };

        //                foreach (var peom in poems)
        //                {
        //                    if (String.IsNullOrEmpty(newAuthor.Name))
        //                    {
        //                        newAuthor.Name = peom.Author;
        //                    }

        //                    newAuthor.Poems.Add(peom.Title);
        //                }
        //                authorsList.Add(newAuthor);
        //            }
        //        }
        //    }
        //    return authorsList;
        //}

        public async Task<IEnumerable<string>> GetAuthors(int numberOfAuthors)
        {
            if (numberOfAuthors <= 0)
                throw new ArgumentOutOfRangeException("Number of authors should be bigger then zero!", nameof(numberOfAuthors));

            AuthorsList? authors = new AuthorsList();
            IEnumerable<string> result;

            var response = await _httpService.GetAsync(_endpoints.Authors);
            if(response.IsSuccessStatusCode) 
            {
                try
                {
                    var res = await response.Content.ReadAsStringAsync();
                    authors = JsonConvert.DeserializeObject<AuthorsList>(res);
                    
                    if (authors != null)
                    {
                        result = authors.Authors.GetRandomAuthors(numberOfAuthors);
                        return result;
                    }
                    else
                    {
                        throw new Exception("List of authors is null or empty");
                    }
                }
                catch (Exception ex) 
                {
                    throw new Exception($"Can't parse authors, {ex.Message}");
                }
            }
            else
            {
                throw new Exception("Can't retrieve authors from server");
            }
        }
    }
}