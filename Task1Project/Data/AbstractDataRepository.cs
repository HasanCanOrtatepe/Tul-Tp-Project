using System.Collections.Generic;
using System.Linq;
using Task1Project.Data.Models.abstracts;
using Task1Project.Data.Models.concretes;

namespace Task1Project.Data
{
    public abstract class AbstractDataRepository
    {
        public abstract List<User> GetUsers();
        public abstract List<Book> GetBooks();
        public abstract List<Order> GetOrders();
        public abstract List<Loan> GetLoans();
        public abstract List<Event> GetEvents();

        public abstract void AddUser(User user);
        public abstract void AddBook(Book book);
        public abstract void AddLoan(Loan loan);
        public abstract void RemoveLoan(Loan loan);
        public abstract void AddOrder(Order order);
        public abstract void AddEvent(Event evt);

        public abstract void GenerateSampleUsers();
        public abstract void GenerateSampleCatalog();

        // Factory metodu
        public static AbstractDataRepository Factory()
        {
            return new CRepository();
        }

        // private inner class
        private class CRepository : AbstractDataRepository
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
        }
    }
}