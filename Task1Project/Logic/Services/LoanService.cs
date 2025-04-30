using System;
using System.Collections.Generic;
using System.Linq;
using Task1Project.Data.Abstract;
using Task1Project.Data.Models;
using Task1Project.Logic.Abstract;

namespace Task1Project.Logic.Services
{
    public class LoanService : AbstractLoanService
    {
        private readonly AbstractUserRepository userRepo;
        private readonly AbstractBookRepository bookRepo;
        private readonly AbstractLoanRepository loanRepo;
        private readonly AbstractEventRepository eventRepo;

        public LoanService(
            AbstractUserRepository userRepo,
            AbstractBookRepository bookRepo,
            AbstractLoanRepository loanRepo,
            AbstractEventRepository eventRepo)
        {
            this.userRepo = userRepo;
            this.bookRepo = bookRepo;
            this.loanRepo = loanRepo;
            this.eventRepo = eventRepo;
        }

        public override void BorrowBook(int userId, int bookId)
        {
            var user = userRepo.GetUsers().FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new Exception($"User with ID {userId} does not exist.");

            var book = bookRepo.GetBooks().FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new Exception($"Book with ID {bookId} does not exist.");

            if (loanRepo.GetLoans().Any(l => l.BookId == bookId))
                throw new Exception($"Book with ID {bookId} is already borrowed.");

            loanRepo.AddLoan(new Loan { UserId = userId, BookId = bookId });

            eventRepo.AddEvent(new Event
            {
                Timestamp = DateTime.Now,
                Description = $"User {userId} borrowed Book {bookId}."
            });
        }

        public override void ReturnBook(int userId, int bookId)
        {
            var loan = loanRepo.GetLoans().FirstOrDefault(l => l.UserId == userId && l.BookId == bookId);
            if (loan == null)
                throw new Exception($"No active loan found for User {userId} and Book {bookId}.");

            loanRepo.RemoveLoan(loan);

            eventRepo.AddEvent(new Event
            {
                Timestamp = DateTime.Now,
                Description = $"User {userId} returned Book {bookId}."
            });
        }

        public override List<Loan> GetActiveLoans()
        {
            return loanRepo.GetLoans();
        }
    }
}
