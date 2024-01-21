using eSky.RecruitmentTask.Controllers;
using eSky.RecruitmentTask.Models;
using eSky.RecruitmentTask.Services;
using eSky.RecruitmentTask.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace eSky.RecruitmentTask.Test.Controllers
{
    public class AuthorControllerTest
    {   
        int NumberOfAuthors;
        IEnumerable<string> Authors;
        IEnumerable<Poem>? Poems;
        IEnumerable<Author> PoemsByAuthor;
        Mock<ILogger<AuthorsController>>? Logger;
        Mock<IAuthorService>? AuthorService;
        AuthorsController? Sut;

        public AuthorControllerTest()
        {   
            NumberOfAuthors = 3;
            Authors = AuthorMockService.GetAuthors(NumberOfAuthors);
            Poems = AuthorMockService.GetPoems(Authors);
            PoemsByAuthor = AuthorMockService.GetPoemsByAuthor(Poems, Authors);
            AuthorService = new Mock<IAuthorService>();
            Logger = new Mock<ILogger<AuthorsController>>();
            Sut = new AuthorsController(AuthorService.Object, Logger.Object);
        }

        [Fact]
        public async Task GetPoemsByAuthor_ShouldReturnStatus200()
        {
            //Arrange
            AuthorService.Setup(_ => _.GetAuthors(NumberOfAuthors)).ReturnsAsync(Authors);
            AuthorService.Setup(_ => _.GetPoems(Authors)).ReturnsAsync(Poems);
            AuthorService.Setup(_ => _.GetPoemsByAuthor(Poems, Authors)).Returns(PoemsByAuthor);
            

            //Act
            var result = await Sut.GetPoemsByAuthor(NumberOfAuthors);

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetPoemsByAuthor_NoAuthors_ShouldReturnStatus204()
        {
            //Arrange
            AuthorService.Setup(_ => _.GetAuthors(NumberOfAuthors)).ReturnsAsync(AuthorMockService.GetAuthorsNoData(NumberOfAuthors));
           
            //Act
            var result = await Sut.GetPoemsByAuthor(NumberOfAuthors);

            //Assert
            result.GetType().Should().Be(typeof(NoContentResult));
            (result as NoContentResult).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task GetPoems_NoPoems_ShouldReturnStatus204()
        {
            //Arrange
            Poems = AuthorMockService.NoPoems(Authors);
            AuthorService.Setup(_ => _.GetAuthors(NumberOfAuthors)).ReturnsAsync(Authors);
            AuthorService.Setup(_ => _.GetPoems(Authors)).ReturnsAsync(Poems);

            //Act
            var result = await Sut.GetPoemsByAuthor(NumberOfAuthors);

            //Assert
            result.GetType().Should().Be(typeof(NoContentResult));
            (result as NoContentResult).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task GetPoemsByAuthor_NoPoems_ShouldReturnStatus204()
        {
            //Arrange
            PoemsByAuthor = AuthorMockService.NoPoemsByAuthor(Poems, Authors);
            AuthorService.Setup(_ => _.GetAuthors(NumberOfAuthors)).ReturnsAsync(Authors);
            AuthorService.Setup(_ => _.GetPoems(Authors)).ReturnsAsync(Poems);
            AuthorService.Setup(_ => _.GetPoemsByAuthor(Poems, Authors)).Returns(PoemsByAuthor);

            //Act
            var result = await Sut.GetPoemsByAuthor(NumberOfAuthors);

            //Assert
            result.GetType().Should().Be(typeof(NoContentResult));
            (result as NoContentResult).StatusCode.Should().Be(204);
        }
    }
}
