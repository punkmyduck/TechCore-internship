using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using task_1135;
using Xunit;

namespace IntegrationTests
{
    public class HelloEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public HelloEndpointTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetHello_ReturnsOk()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Hello");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
