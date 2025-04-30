using System.Collections.Generic;
using Task1Project.Data.Models;
using Task1Project.Data.Abstract;

namespace Task1Project.Data.Repositories.InMemory
{
    public class InMemoryLoanRepository : AbstractLoanRepository
    {
        private readonly List<Loan> loans = new();

        public override List<Loan> GetLoans() => loans;

        public override void AddLoan(Loan loan) => loans.Add(loan);

        public override void RemoveLoan(Loan loan) => loans.Remove(loan);
    }
}
