using System.Collections.Generic;
using Task1Project.Data.Models.abstracts;
using Task1Project.Data.Models.concretes;

namespace Task1Project.Data
{
    internal class InMemoryDataRepository : AbstractDataRepository

    {
        private List<User> users = new();
        private List<Book> books = new();
        private List<Order> orders = new();
        private List<Loan> loans = new();
        private List<Event> events = new();

        public override List<User> GetUsers() => users;
        public override List<Book> GetBooks() => books;
        public override List<Order> GetOrders() => orders;
        public override List<Loan> GetLoans() => loans;
        public override List<Event> GetEvents() => events;

        public override void AddUser(User user) => users.Add(user);
        public override void AddBook(Book book) => books.Add(book);
        public override void AddLoan(Loan loan) => loans.Add(loan);
        public override void RemoveLoan(Loan loan) => loans.Remove(loan);
        public override void AddOrder(Order order) => orders.Add(order);
        public override void AddEvent(Event evt) => events.Add(evt);

        public override void GenerateSampleUsers()
        {
            users.Add(new ConcreteUser { Id = 1, Name = "Alice" });
            users.Add(new ConcreteUser { Id = 2, Name = "Bob" });
        }

        public override void GenerateSampleCatalog()
        {
            books.Add(new ConcreteBook { Id = 101, Title = "C# Basics", Author = "John Doe" });
            books.Add(new ConcreteBook { Id = 102, Title = "Advanced .NET", Author = "Jane Smith" });
        }

        // Factory metodu
        public static AbstractDataRepository Factory()
        {
            return new ConcreteInMemoryDataRepository();
        }

        // İç sınıf: Veritabanı işlemleri burada yapılacak, dışarıya erişilemez.
        private class ConcreteInMemoryDataRepository : InMemoryDataRepository
        {
            // Bu sınıf sadece InMemoryDataRepository içinde kullanılacak
        }
    }
}