using System;
using System.Linq;
using System.Collections.Generic;
using Task1Project.Data;
using Task1Project.Data.Models.abstracts;
using Task1Project.Data.Models.concretes;

namespace Task1Project.Logic
{
    internal class BookstoreService : AbstractBookstoreService
    {
        private readonly AbstractDataRepository repo;

        public BookstoreService(AbstractDataRepository repository)
        {
            repo = repository;
        }

        public override List<Book> BrowseBooks()
        {
            return repo.GetBooks();
        }

        public override void PlaceOrder(int userId, List<int> bookIds)
        {
            var user = repo.GetUsers().FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new Exception($"User with ID {userId} does not exist.");

            foreach (var bookId in bookIds)
            {
                var book = repo.GetBooks().FirstOrDefault(b => b.Id == bookId);
                if (book == null)
                    throw new Exception($"Book with ID {bookId} does not exist.");
            }

            var order = new ConcreteOrder
            {
                Id = repo.GetOrders().Count > 0 ? repo.GetOrders().Max(o => o.Id) + 1 : 1,
                UserId = userId,
                BookIds = bookIds
            };

            repo.AddOrder(order);
            repo.AddEvent(new ConcreteEvent
            {
                Timestamp = DateTime.Now,
                Description = $"User {userId} placed an order for books: {string.Join(", ", bookIds)}"
            });
        }

        public override void BorrowBook(int userId, int bookId)
        {
            var user = repo.GetUsers().FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new Exception($"User with ID {userId} does not exist.");

            var book = repo.GetBooks().FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new Exception($"Book with ID {bookId} does not exist.");

            if (repo.GetLoans().Any(l => l.BookId == bookId))
                throw new Exception($"Book with ID {bookId} is already borrowed.");

            var loan = new ConcreteLoan
            {
                UserId = userId,
                BookId = bookId
            };

            repo.AddLoan(loan);
            repo.AddEvent(new ConcreteEvent
            {
                Timestamp = DateTime.Now,
                Description = $"User {userId} borrowed Book {bookId}."
            });
        }

        public override void ReturnBook(int userId, int bookId)
        {
            var loan = repo.GetLoans().FirstOrDefault(l => l.UserId == userId && l.BookId == bookId);
            if (loan == null)
                throw new Exception($"No active loan found for User {userId} and Book {bookId}.");

            repo.RemoveLoan(loan);
            repo.AddEvent(new ConcreteEvent
            {
                Timestamp = DateTime.Now,
                Description = $"User {userId} returned Book {bookId}."
            });
        }

        public override List<Loan> GetActiveLoans()
        {
            return repo.GetLoans();
        }
    }
}
