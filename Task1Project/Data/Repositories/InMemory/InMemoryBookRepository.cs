using System.Collections.Generic;
using Task1Project.Data.Models;
using Task1Project.Data.Abstract;

namespace Task1Project.Data.Repositories.InMemory
{
    public class InMemoryBookRepository : AbstractBookRepository
    {
        private readonly List<Book> books = new();

        public override List<Book> GetBooks() => books;

        public override void AddBook(Book book) => books.Add(book);

        public override void GenerateSampleCatalog()
        {
            books.Add(new Book { Id = 101, Title = "C# Basics", Author = "John Doe" });
            books.Add(new Book { Id = 102, Title = "Advanced .NET", Author = "Jane Smith" });
        }
    }
}
