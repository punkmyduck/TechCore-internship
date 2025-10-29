using task_1135.Domain.Services;

namespace task_1135.Application.Services
{
    public class JsonPlaceholderService : IJsonPlaceholderService
    {
        private readonly IHttpClientFactory _factory;
        public JsonPlaceholderService(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        public async Task<string> GetTodosJson()
        {
            var client = _factory.CreateClient("JsonPlaceholder");
            var response = await client.GetAsync("todos/1");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
