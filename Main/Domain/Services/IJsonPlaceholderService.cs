namespace task_1135.Domain.Services
{
    public interface IJsonPlaceholderService
    {
        Task<string> GetTodosJson();
    }
}
