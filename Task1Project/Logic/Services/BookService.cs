using System.Collections.Generic;
using Task1Project.Data.Abstract;
using Task1Project.Data.Models;
using Task1Project.Logic.Abstract;

namespace Task1Project.Logic.Services
{
    public class BookService : AbstractBookService
    {
        private readonly AbstractBookRepository bookRepo;

        public BookService(AbstractBookRepository bookRepo)
        {
            this.bookRepo = bookRepo;
        }

        public override List<Book> BrowseBooks()
        {
            return bookRepo.GetBooks();
        }
    }
}
