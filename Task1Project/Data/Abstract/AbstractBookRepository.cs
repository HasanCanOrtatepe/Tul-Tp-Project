using System.Collections.Generic;
using Task1Project.Data.Models;

namespace Task1Project.Data.Abstract
{
    public abstract class AbstractBookRepository
    {
        public abstract List<Book> GetBooks();
        public abstract void AddBook(Book book);
        public abstract void GenerateSampleCatalog();
    }
}
