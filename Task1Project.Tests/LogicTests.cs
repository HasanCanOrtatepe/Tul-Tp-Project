using Xunit;
using System.Collections.Generic;
using Task1Project.Data;
using Task1Project.Data.Models.abstracts;
using Task1Project.Logic;

namespace Task1Project.Tests
{
    public class LogicTests
    {
        [Fact]
        public void PlaceOrder_ShouldAddOrderWithCorrectUserAndBooks()
        {
            // Arrange
            var repo = new InMemoryDataRepository();
            repo.GenerateSampleUsers();
            repo.GenerateSampleCatalog();
            var service = new BookstoreService(repo);

            // Act
            service.PlaceOrder(1, new List<int> { 101, 102 });

            // Assert
            var orders = repo.GetOrders();
            Assert.Single(orders);
            Assert.Equal(1, orders[0].UserId);
            Assert.Equal(2, orders[0].BookIds.Count);
            Assert.Contains(101, orders[0].BookIds);
            Assert.Contains(102, orders[0].BookIds);
        }

        [Fact]
        public void BorrowAndReturnBook_ShouldTrackLoanCorrectly()
        {
            // Arrange
            var repo = new InMemoryDataRepository();
            repo.GenerateSampleUsers();
            repo.GenerateSampleCatalog();
            var service = new BookstoreService(repo);

            // Act
            service.BorrowBook(1, 101);

            // Assert Borrow
            var loans = repo.GetLoans();
            Assert.Single(loans);
            Assert.Equal(1, loans[0].UserId);
            Assert.Equal(101, loans[0].BookId);

            // Act Return
            service.ReturnBook(1, 101);

            // Assert Return
            Assert.Empty(repo.GetLoans());
        }

        [Fact]
        public void BorrowAlreadyBorrowedBook_ShouldThrowExceptionWithMeaningfulMessage()
        {
            // Arrange
            var repo = new InMemoryDataRepository();
            repo.GenerateSampleUsers();
            repo.GenerateSampleCatalog();
            var service = new BookstoreService(repo);

            service.BorrowBook(1, 101);

            // Act & Assert
            var ex = Assert.Throws<System.Exception>(() => service.BorrowBook(2, 101));
            Assert.Contains("already borrowed", ex.Message, System.StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void ReturnBook_ThatWasNotBorrowed_ShouldThrowException()
        {
            // Arrange
            var repo = new InMemoryDataRepository();
            repo.GenerateSampleUsers();
            repo.GenerateSampleCatalog();
            var service = new BookstoreService(repo);

            // Act & Assert
            var ex = Assert.Throws<System.Exception>(() => service.ReturnBook(1, 101));
            Assert.Contains("No active loan", ex.Message, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}
