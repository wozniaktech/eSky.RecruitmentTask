using eSky.RecruitmentTask.Controllers;
using eSky.RecruitmentTask.Services;
using eSky.RecruitmentTask.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace eSky.RecruitmentTask.Test.Controllers
{
    public class AuthorControllerTest
    {
        [Fact]
        public async Task GetPoemsByAuthor_ShouldReturnStatus200()
        {
            //Arrange
            int numberOfAuthors = 3;
            var authors = AuthorMockService.GetAuthors(numberOfAuthors);
            var authorService = new Mock<IAuthorService>();
            authorService.Setup(_ => _.GetAuthors(numberOfAuthors)).ReturnsAsync(AuthorMockService.GetAuthors(numberOfAuthors));
            //authorService.Setup(_ => _.GetPoemsByAuthor(authors)).ReturnsAsync(AuthorMockService.GetPoemsByAuthor(authors));
            var sut = new AuthorsController(authorService.Object);

            //Act
            var result = await sut.GetPoemsByAuthor(numberOfAuthors);

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetPoemsByAuthor_NoAuthors_ShouldReturnStatus204()
        {
            //Arrange
            int numberOfAuthors = 3;
            var authors = AuthorMockService.GetAuthors(numberOfAuthors);
            var authorService = new Mock<IAuthorService>();
            authorService.Setup(_ => _.GetAuthors(numberOfAuthors)).ReturnsAsync(AuthorMockService.GetAuthorsNoData(numberOfAuthors));
            //authorService.Setup(_ => _.GetPoemsByAuthor(authors)).ReturnsAsync(AuthorMockService.GetPoemsByAuthor(authors));
            var sut = new AuthorsController(authorService.Object);

            //Act
            var result = await sut.GetPoemsByAuthor(numberOfAuthors);

            //Assert
            result.GetType().Should().Be(typeof(NoContentResult));
            (result as NoContentResult).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task GetPoemsByAuthor_NoPoems_ShouldReturnStatus204()
        {
            //Arrange
            int numberOfAuthors = 3;
            var authors = AuthorMockService.GetAuthors(numberOfAuthors);
            var authorService = new Mock<IAuthorService>();
            authorService.Setup(_ => _.GetAuthors(numberOfAuthors)).ReturnsAsync(AuthorMockService.GetAuthors(numberOfAuthors));
            //authorService.Setup(_ => _.GetPoemsByAuthor(authors)).ReturnsAsync(AuthorMockService.GetPoemsByAuthorNoData(authors));
            var sut = new AuthorsController(authorService.Object);

            //Act
            var result = await sut.GetPoemsByAuthor(numberOfAuthors);

            //Assert
            result.GetType().Should().Be(typeof(NoContentResult));
            (result as NoContentResult).StatusCode.Should().Be(204);
        }
    }
}
