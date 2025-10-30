using Domain.DTOs;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;

namespace task1135.Application.Services
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

            await _authorRepository.AddAsync(author);
            await _authorRepository.SaveChangesAsync();

            return author;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return false;
            }
            
            await _authorRepository.DeleteByIdAsync(id);
            await _authorRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }
    }
}
