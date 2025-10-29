using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using task_1135.Application.DTOs;
using Xunit;

namespace IntegrationTests
{
    public class BooksValidationTests : IClassFixture<MyTestFactory>
    {
        private readonly MyTestFactory _factory;
        public BooksValidationTests(MyTestFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PostBook_ReturnsBadRequest_WhenDtoInvalid()
        {
            var client = _factory.CreateClient();

            // Некорректный DTO
            var badDto = new CreateBookDto("Some book", -1500);

            // Act
            var response = await client.PostAsJsonAsync("/Book", badDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
