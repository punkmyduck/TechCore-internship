using Domain.Services;

namespace task1135.Application.Services
{
    public class JsonPlaceholderService : IJsonPlaceholderService
    {
        private readonly HttpClient _httpClient;
        public JsonPlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetTodosJson()
        {
            var response = await _httpClient.GetAsync("todos/1");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}