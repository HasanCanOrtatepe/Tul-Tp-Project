using Xunit;
using Task1Project.Data.Repositories.InMemory;

namespace Task1Project.Tests
{
    public class DataTests
    {
        [Fact]
        public void GenerateSampleUsers_ShouldCreateUsers()
        {
            var userRepo = new InMemoryUserRepository();
            userRepo.GenerateSampleUsers();

            Assert.True(userRepo.GetUsers().Count >= 2, "Sample users generation failed.");
        }

        [Fact]
        public void GenerateSampleCatalog_ShouldCreateBooks()
        {
            var bookRepo = new InMemoryBookRepository();
            bookRepo.GenerateSampleCatalog();

            Assert.True(bookRepo.GetBooks().Count >= 2, "Sample catalog generation failed.");
        }
    }
}
