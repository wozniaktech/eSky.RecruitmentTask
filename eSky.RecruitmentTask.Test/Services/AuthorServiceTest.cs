using eSky.RecruitmentTask.Config;
using eSky.RecruitmentTask.Helper;
using eSky.RecruitmentTask.Models;
using eSky.RecruitmentTask.Services;
using eSky.RecruitmentTask.Test.MockData;
using FluentAssertions;
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
            Config = AuthorMockService.ReturnEndpointConfig();
            Monitor = Mock.Of<IOptionsMonitor<EndpointConfig>>(_ => _.CurrentValue == Config);
        }

        [Fact]
        public async Task GetAuthors_ShoudReturn_ListOfAuthors()
        {
            //arrange
            var authors = AuthorMockService.GetAuthorsJson();
            var sut = new AuthorService(HttpService.Object, Monitor, Logger.Object);
            HttpService.Setup(_ => _.GetAsync(Monitor.CurrentValue.Authors)).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(authors) });

            //act
            var result = await sut.GetAuthors(NumberOfAuthors);

            //assert
            Assert.NotEmpty(result);
            result.GetType().Should().Be(typeof(List<string>));
        }

        [Fact]
        public async Task GetAuthors_NoAuthorsFound_ReturnsException()
        {
            //arrange
            var sut = new AuthorService(HttpService.Object, Monitor, Logger.Object);
            HttpService.Setup(_ => _.GetAsync(Monitor.CurrentValue.Authors)).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound});

            //act
            var result = ()=> sut.GetAuthors(NumberOfAuthors);
                        
            //assert
            var ex = await Assert.ThrowsAsync<Exception>(result);
            Assert.Equal(ErrorMessages.CANNOT_RETRIEVE_AUTHORS_FROM_SERVER,ex.Message);
        }

        [Fact]
        public async Task GetPoems_ShouldReturn_ListOfPoems()
        {
            //arrange
            var listOfAuthors = AuthorMockService.ReturnListOfAuthors();
            var listOfpoems = AuthorMockService.ReturnListOfPoemsJson();
            var sut = new AuthorService(HttpService.Object, Monitor, Logger.Object);
            HttpService.Setup(_ => _.GetAsync(Monitor.CurrentValue.Poems + listOfAuthors.ElementAt(0))).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(listOfpoems[0]) });
            HttpService.Setup(_ => _.GetAsync(Monitor.CurrentValue.Poems + listOfAuthors.ElementAt(1))).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(listOfpoems[1]) });
            HttpService.Setup(_ => _.GetAsync(Monitor.CurrentValue.Poems + listOfAuthors.ElementAt(2))).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(listOfpoems[2]) });

            //act
            var result = await sut.GetPoems(listOfAuthors);

            //assert
            Assert.NotEmpty(result);
            result.GetType().Should().Be(typeof(List<Poem>));
        }

        [Fact]
        public async Task GetPoems_PoemsOfFirstAuthorWasNotFound_ShouldReturnCorrectListOfPoems()
        {
            //arrange
            var listOfAuthors = AuthorMockService.ReturnListOfAuthors();
            var listOfPoems = AuthorMockService.ReturnListOfPoemsJson();
            var sut = new AuthorService(HttpService.Object, Monitor, Logger.Object);
            HttpService.Setup(_ => _.GetAsync(Monitor.CurrentValue.Poems + listOfAuthors.ElementAt(0))).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound, Content = new StringContent("") });
            HttpService.Setup(_ => _.GetAsync(Monitor.CurrentValue.Poems + listOfAuthors.ElementAt(1))).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(listOfPoems[1]) });
            HttpService.Setup(_ => _.GetAsync(Monitor.CurrentValue.Poems + listOfAuthors.ElementAt(2))).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(listOfPoems[2]) });

            //act
            var result = await sut.GetPoems(listOfAuthors);

            //assert
            Assert.NotEmpty(result);
            result.GetType().Should().Be(typeof(List<Poem>));
            Assert.False(result.Any(result=>result.Author==listOfAuthors.ElementAt(0)));
            Assert.True(result.Any(result => result.Author == listOfAuthors.ElementAt(1)));
            Assert.True(result.Any(result => result.Author == listOfAuthors.ElementAt(2)));
        }

        [Fact]
        public void GetPoemsByAuthor_ShouldReturnListOfAuthors()
        {
            //arrange
            var sut = new AuthorService(HttpService.Object, Monitor, Logger.Object);
            var authors = AuthorMockService.GetAuthors(3);
            var poems = AuthorMockService.GetPoems(authors);

            //act
            var result = sut.GetPoemsByAuthor(poems, authors);

            //assert
            Assert.NotEmpty(result);
            result.GetType().Should().Be(typeof(List<Author>));
            Assert.True(result.Any(result => result.Name == authors.ElementAt(0)));
            Assert.True(result.Any(result => result.Name == authors.ElementAt(1)));
            Assert.True(result.Any(result => result.Name == authors.ElementAt(2)));
        }
    }
}