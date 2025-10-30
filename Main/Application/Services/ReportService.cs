using System.Data;
using Dapper;
using Npgsql;
using Domain.DTOs;
using Domain.Services;

namespace task1135.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IConfiguration _configuration;
        public ReportService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private IDbConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new NpgsqlConnection(connectionString);
        }
        public async Task<IEnumerable<ReportDto>> GetBooksPerAuthorAsync()
        {
            const string sql = @"
                SELECT a.""Name"" AS AuthorName,
                       COUNT(ba.""BooksId"") AS BooksCount
                FROM ""Authors"" a
                LEFT JOIN ""BookAuthors"" ba ON a.""Id"" = ba.""AuthorsId""
                GROUP BY a.""Name""
                ORDER BY BooksCount DESC;
            ";

            using var connection = CreateConnection();
            var result = await connection.QueryAsync<ReportDto>(sql);
            return result;
        }
    }
}
