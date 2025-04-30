using System.Collections.Generic;
using Task1Project.Data.Models;

namespace Task1Project.Logic.Abstract
{
    public abstract class AbstractLoanService
    {
        public abstract void BorrowBook(int userId, int bookId);
        public abstract void ReturnBook(int userId, int bookId);
        public abstract List<Loan> GetActiveLoans();
    }
}
