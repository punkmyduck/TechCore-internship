namespace Domain.Services
{
    public interface IJsonPlaceholderService
    {
        Task<string> GetTodosJson();
    }
}
