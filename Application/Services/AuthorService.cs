using task_1135.Application.DTOs;
using task_1135.Domain.Models;
using task_1135.Domain.Repositories;
using task_1135.Domain.Services;

namespace task_1135.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<Author> AddAsync(CreateAuthorDto createAuthorDto)
        {
            var author = new Author
            {
                Name = createAuthorDto.Name
            };

            await _authorRepository.Add(author);

            return author;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var author = await _authorRepository.GetById(id);
            if (author == null)
            {
                return false;
            }
            await _authorRepository.DeleteById(id);
            return true;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _authorRepository.GetAll();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _authorRepository.GetById(id);
        }
    }
}
