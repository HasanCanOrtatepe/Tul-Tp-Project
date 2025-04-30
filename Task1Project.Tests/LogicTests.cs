using Xunit;
using System.Collections.Generic;
using System.Linq;
using Task1Project.Data.Repositories.InMemory;
using Task1Project.Logic.Services;

namespace Task1Project.Tests
{
    public class LogicTests
    {
        [Fact]
        public void PlaceOrder_ShouldAddOrder()
        {
            // Arrange
            var userRepo = new InMemoryUserRepository();
            var bookRepo = new InMemoryBookRepository();
            var orderRepo = new InMemoryOrderRepository();
            var eventRepo = new InMemoryEventRepository();

            userRepo.GenerateSampleUsers();
            bookRepo.GenerateSampleCatalog();

            var orderService = new OrderService(userRepo, bookRepo, orderRepo, eventRepo);

            // Act
            orderService.PlaceOrder(1, new List<int> { 101, 102 });

            // Assert
            Assert.Single(orderRepo.GetOrders());
        }

        [Fact]
        public void BorrowAndReturnBook_ShouldManageLoans()
        {
            // Arrange
            var userRepo = new InMemoryUserRepository();
            var bookRepo = new InMemoryBookRepository();
            var loanRepo = new InMemoryLoanRepository();
            var eventRepo = new InMemoryEventRepository();

            userRepo.GenerateSampleUsers();
            bookRepo.GenerateSampleCatalog();

            var loanService = new LoanService(userRepo, bookRepo, loanRepo, eventRepo);

            // Act
            loanService.BorrowBook(1, 101);

            // Assert borrow
            Assert.Single(loanRepo.GetLoans());

            // Act return
            loanService.ReturnBook(1, 101);

            // Assert return
            Assert.Empty(loanRepo.GetLoans());
        }

        [Fact]
        public void BorrowAlreadyBorrowedBook_ShouldThrowException()
        {
            // Arrange
            var userRepo = new InMemoryUserRepository();
            var bookRepo = new InMemoryBookRepository();
            var loanRepo = new InMemoryLoanRepository();
            var eventRepo = new InMemoryEventRepository();

            userRepo.GenerateSampleUsers();
            bookRepo.GenerateSampleCatalog();

            var loanService = new LoanService(userRepo, bookRepo, loanRepo, eventRepo);

            loanService.BorrowBook(1, 101);

            // Act & Assert
            var ex = Assert.Throws<System.Exception>(() => loanService.BorrowBook(2, 101));
            Assert.Contains("already borrowed", ex.Message);
        }
    }
}
