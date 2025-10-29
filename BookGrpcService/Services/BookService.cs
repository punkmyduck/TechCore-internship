using AuthorGrpcService;
using Grpc.Net.Client;

namespace BookGrpcService.Services
{
    public class BookService
    {
        private readonly ILogger<BookService> _logger;
        public BookService(ILogger<BookService> logger)
        {
            _logger = logger;
        }

        public async Task GetAuthorInfoAsync(int id)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5226");
            var client = new AuthorGrpcService.AuthorService.AuthorServiceClient(channel);
            var reply = await client.GetAuthorAsync(new AuthorRequest { Id = 1 });
            _logger.LogInformation("Author Info: {id} {Name}", reply.Id, reply.Name);
        }
    }
}
