using Task1Project.Data;
using Task1Project.Data.Models.abstracts;
using Task1Project.Data.Models.concretes;
using Xunit;

public class DataTests
{
    [Fact]
    public void GenerateSampleUsers_ShouldCreateTwoSpecificUsers()
    {
        var repo = new InMemoryDataRepository();
        repo.GenerateSampleUsers();

        var users = repo.GetUsers();

        Assert.Equal(2, users.Count);
        Assert.Contains(users, u => u.Name == "Alice");
        Assert.Contains(users, u => u.Name == "Bob");
    }

    [Fact]
    public void GenerateSampleCatalog_ShouldCreateExpectedBooks()
    {
        var repo = new InMemoryDataRepository();
        repo.GenerateSampleCatalog();

        var books = repo.GetBooks();

        Assert.Equal(2, books.Count);
        Assert.Contains(books, b => b.Title == "C# Basics" && b.Author == "John Doe");
        Assert.Contains(books, b => b.Title == "Advanced .NET" && b.Author == "Jane Smith");
    }

    [Fact]
    public void AddUser_ShouldIncreaseUserCount()
    {
        var repo = new InMemoryDataRepository();
        int countBefore = repo.GetUsers().Count;

        repo.AddUser(new ConcreteUser { Id = 3, Name = "Charlie" });

        var users = repo.GetUsers();

        Assert.Equal(countBefore + 1, users.Count);
        Assert.Contains(users, u => u.Name == "Charlie");
    }
}
