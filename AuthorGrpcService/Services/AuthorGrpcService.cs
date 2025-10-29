using Grpc.Core;

namespace AuthorGrpcService.Services
{
    public class AuthorGrpcServiceImpl : AuthorService.AuthorServiceBase
    {
        public override Task<AuthorReply> GetAuthor(AuthorRequest request, ServerCallContext context)
        {
            var author = new AuthorReply
            {
                Id = request.Id,
                Name = "Some dude"
            };

            return Task.FromResult(author);
        }
    }
}
