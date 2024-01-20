﻿using eSky.RecruitmentTask.Config;
using eSky.RecruitmentTask.Helper;
using eSky.RecruitmentTask.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace eSky.RecruitmentTask.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IHttpService _httpService;
        private readonly ILogger _logger;
        private readonly EndpointConfig _endpoints;

        public AuthorService(IHttpService httpService, 
            IOptionsMonitor<EndpointConfig> endpointConfig, 
            ILogger<AuthorService> logger)
        {
            _httpService = httpService;
            _endpoints = endpointConfig.CurrentValue;
            _logger = logger;
        }

        public async Task<IEnumerable<string>> GetAuthors(int numberOfAuthors)
        {
            if (numberOfAuthors <= 0)
            {
                _logger.LogError(ErrorMessagesHelper.INCORRECT_NUMBER_OF_AUTHORS);
                throw new ArgumentOutOfRangeException(ErrorMessagesHelper.INCORRECT_NUMBER_OF_AUTHORS, 
                    nameof(numberOfAuthors));
            }
                
            AuthorsList? authors = new AuthorsList();
            IEnumerable<string> result = Enumerable.Empty<string>();

            var response = await _httpService.GetAsync(_endpoints.Authors);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var res = await response.Content.ReadAsStringAsync();
                    authors = JsonConvert.DeserializeObject<AuthorsList>(res);

                    if (authors != null)
                    {
                        result = authors.Authors.GetRandomAuthors(numberOfAuthors);
                        
                    }
                    return result;
                }
                catch (Exception)
                {
                    _logger.LogError(ErrorMessagesHelper.CANNOT_DESERIALIZE_AUTHORS_LIST);
                    throw new Exception(ErrorMessagesHelper.CANNOT_DESERIALIZE_AUTHORS_LIST);
                }
            }
            else
            {
                _logger.LogError(ErrorMessagesHelper.CANNOT_RETRIEVE_AUTHORS_FROM_SERVER);
                throw new Exception(ErrorMessagesHelper.CANNOT_RETRIEVE_AUTHORS_FROM_SERVER);
            }
        }

        public async Task<IEnumerable<Poem>> GetPoems(IEnumerable<string> authors)
        {
            if (string.IsNullOrEmpty(_endpoints.Authors))
                throw new ArgumentOutOfRangeException("List of authors can't by null or empty", nameof(authors));

            //I used list below because I needed AddRange method, which is not available in IEnumerable, ICollection nor IList
            List<Poem> poems = new List<Poem>();

            if (authors.Any())
            {
                var poemsTasks = authors.Select(author => _httpService.GetAsync(_endpoints.Poems + author));
                var poemsResponses = await Task.WhenAll(poemsTasks);

                if (poemsResponses.Any())
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

        public IEnumerable<Author> GetPoemsByAuthor(IEnumerable<Poem>? poems, IEnumerable<string> authors)
        {
            if ((poems == null) || (!poems.Any()))
                throw new ArgumentOutOfRangeException("List of poems can't by null or empty", nameof(poems));

            if (string.IsNullOrEmpty(_endpoints.Authors))
                throw new ArgumentOutOfRangeException("List of authors can't by null or empty", nameof(authors));

            ICollection<Author> authorsList = new List<Author>();

            foreach (var author in authors) 
            {
                var newAuthor = new Author()
                {
                    Name = author,
                    Poems = new List<string>()
                };

                foreach (var poem in poems)
                {
                    if (newAuthor.Name == poem.Author )
                    {
                        newAuthor.Poems.Add(poem.Title);
                    }
                }
                authorsList.Add(newAuthor);
            }
            return authorsList;
        }
    }
}