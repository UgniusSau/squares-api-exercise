using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using SquareAPI.Services.Tests.Unit.Middleware;

namespace SquaresAPI.Tests.Middleware
{
    public class RequestTimeoutMiddlewareTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public RequestTimeoutMiddlewareTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RequestTimeoutMiddleware_Should_Return_408_When_Request_Times_Out()
        {
            // Act
            var response = await _client.GetAsync("/api/test/v1/points");

            // Assert
            Assert.Equal(StatusCodes.Status408RequestTimeout, (int)response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("Request timed out.", content);
        }
    }
}
