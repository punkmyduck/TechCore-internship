using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using task1135.Application.Services;
using Domain.Models;
using Domain.Repositories;
using Xunit;

namespace Tests
{
    public class BookServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_ReturnsBook_WhenBookExistsAndNotCached()
        {
            var book = new Book
            {
                Id = 1,
                Title = "test book",
                YearPublished = 2008,
                Authors = new List<Author> { new Author { Id = 1, Name = "no name chelik" } }
            };

            var repoMock = new Mock<IBookRepository>();
            repoMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(book);

            var cacheMock = new Mock<IDistributedCache>();
            cacheMock
                .Setup(c => c.GetAsync("book:1", It.IsAny<CancellationToken>()))
                .ReturnsAsync((byte[]?)null);

            var reviewRepoMock = new Mock<IProductReviewRepository>();

            var service = new BookService(repoMock.Object, cacheMock.Object, reviewRepoMock.Object);

            var result = await service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Title, result.Title);
            Assert.Equal(book.YearPublished, result.YearPublished);

            repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            cacheMock.Verify(c => c.GetAsync("book:1", It.IsAny<CancellationToken>()), Times.Once);
            cacheMock.Verify(c => c.SetAsync(
                "book:1",
                It.IsAny<byte[]>(),
                It.IsAny<DistributedCacheEntryOptions>(),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
