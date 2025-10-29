using System.Net;
using Xunit;

namespace IntegrationTests
{
    public class BooksIntegrationTests : IClassFixture<MyTestFactory>
    {
        private readonly MyTestFactory _factory;
        public BooksIntegrationTests(MyTestFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllBooks_ReturnsOk()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Author");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
