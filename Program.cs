
using task_1135.Application.Services;
using task_1135.Domain.Repositories;
using task_1135.Domain.Services;
using task_1135.Infrastructure.Middlewares;
using task_1135.Infrastructure.Repositories;
using task_1135.Infrastructure.Storage;

namespace task_1135
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            // Register application services
            builder.Services.AddScoped<ITimeService, TimeService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddSingleton<ILogService, ConsoleLogService>();

            // Register infrastructure services
            builder.Services.AddSingleton<BookStorage>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<TimingMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
