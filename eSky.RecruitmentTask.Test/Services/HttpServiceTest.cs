using eSky.RecruitmentTask.Helper;
using eSky.RecruitmentTask.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

namespace eSky.RecruitmentTask.Test.Services
{
    public class HttpServiceTest
    {
        string url;
        Mock<HttpMessageHandler>? handlerMock;
        HttpResponseMessage? mockResponse;
        HttpClient? Client;
        Mock<IHttpClientFactory>? MockHttpClientFactory;
        Mock<ILogger<HttpService>>? Logger;
        
        public HttpServiceTest()
        {
            url = "http://someurl.com";
            handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            Client = new HttpClient(handlerMock.Object) {BaseAddress = new Uri(url)};
            MockHttpClientFactory = new Mock<IHttpClientFactory>();
            Logger = new Mock<ILogger<HttpService>>();
        }

        [Fact]
        public async Task GetAsync_ReturnsCorrectResponse_ShouldReturnStatus200()
        {
            //arrange
            mockResponse = new HttpResponseMessage {StatusCode = System.Net.HttpStatusCode.OK};
            handlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(mockResponse);
            MockHttpClientFactory.Setup(_ => _.CreateClient("")).Returns(Client);
            var service = new HttpService(MockHttpClientFactory.Object, Logger.Object);
      
            //act
            var result = await service.GetAsync(url);

            //assert
            result.GetType().Should().Be(typeof(HttpResponseMessage));
            (result as HttpResponseMessage).StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAsync_InternalServerError_ServiceShouldReturnStatus500()
        {
            //arrange
            mockResponse = new HttpResponseMessage {StatusCode = System.Net.HttpStatusCode.InternalServerError};
            handlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(mockResponse);
            MockHttpClientFactory.Setup(_ => _.CreateClient("")).Returns(Client);
            var service = new HttpService(MockHttpClientFactory.Object, Logger.Object);

            //act
            var result = await service.GetAsync(url);

            //assert
            result.GetType().Should().Be(typeof(HttpResponseMessage));
            (result as HttpResponseMessage).StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task GetAsync_ExceptionOccurred_ShouldThrowException()
        {
            //arrange
            mockResponse = new HttpResponseMessage();
            handlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ThrowsAsync(new Exception());
            MockHttpClientFactory.Setup(_ => _.CreateClient("")).Returns(Client);
            var service = new HttpService(MockHttpClientFactory.Object, Logger.Object);

            //act
            var result =()=> service.GetAsync(url);

            //assert
            var ex = await Assert.ThrowsAsync<Exception>(result);
            Assert.Equal(ErrorMessages.CANT_RETREIVE_DATA_FROM_SERVER, ex.Message);
        }
    }
}
