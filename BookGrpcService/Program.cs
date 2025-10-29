using BookGrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<BookGrpcService.Services.BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.



app.MapGet("/", async (BookGrpcService.Services.BookService bookService) =>
{
    await bookService.GetAuthorInfoAsync(1);
    return "BookGrpcService called AuthorGrpcService";
});

app.Run();
