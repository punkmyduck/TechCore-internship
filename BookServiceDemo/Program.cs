using Persistence.Extensions;
using BookServiceDemo.Extensions;

namespace BookServiceDemo
{
    /*/
     * ƒанный сервис представл€ет собой упрощенный
     * оригинальный BookService из учебного проекта,
     * но без множества реализованных сервисов
     * и с минимальным функционалом
     * дл€ тестировани€.
     * /*/
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

            builder.AddDatabaseConfiguration();
            builder.Services.AddServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || true)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
