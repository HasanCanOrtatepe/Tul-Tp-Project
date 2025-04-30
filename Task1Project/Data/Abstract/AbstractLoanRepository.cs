using System.Collections.Generic;
using Task1Project.Data.Models;

namespace Task1Project.Data.Abstract
{
    public abstract class AbstractLoanRepository
    {
        public abstract List<Loan> GetLoans();
        public abstract void AddLoan(Loan loan);
        public abstract void RemoveLoan(Loan loan);
    }
}
