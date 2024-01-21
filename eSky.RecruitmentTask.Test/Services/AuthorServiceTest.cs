using eSky.RecruitmentTask.Config;
using eSky.RecruitmentTask.Controllers;
using eSky.RecruitmentTask.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;

namespace eSky.RecruitmentTask.Test.Services
{
    public class AuthorServiceTest
    {
        int NumberOfAuthors = 3;
        Mock<IHttpService> HttpService;
        EndpointConfig Config;
        IOptionsMonitor<EndpointConfig>? Monitor;
        Mock<ILogger<AuthorService>> Logger;

        public AuthorServiceTest()
        {
            Logger = new Mock<ILogger<AuthorService>>();
            HttpService = new Mock<IHttpService>();

            Config = new EndpointConfig 
            {
                Authors = "http://authors.com",
                Poems = "http://poems.com"
            };
            Monitor = Mock.Of<IOptionsMonitor<EndpointConfig>>(_ => _.CurrentValue == Config);

            

        }

        [Fact]
        public async Task GetAuthors_ShoudReturn_IEnumerableAuthors()
        {
            //arrange
            IEnumerable<string> result1 = new List<string>
            {
                "Stefan","Roman","Tadek"
            };

            //Task<HttpResponseMessage> response = new Task<HttpResponseMessage>( HttpStatusCode.OK)
            //{
                
            //};

            AuthorService sut = new AuthorService(HttpService.Object, Monitor, Logger.Object);
            //HttpService.Setup(_ => _.GetAsync(Monitor.CurrentValue.Authors)).Returns(response);
            

            //act
            var result = await sut.GetAuthors(NumberOfAuthors);

            //var optionsMonitor = new OptionsMonitor(options);

        }
    }
}
