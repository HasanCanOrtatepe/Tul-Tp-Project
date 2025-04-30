using System;
using System.Linq;
using System.Collections.Generic;
using Task1Project.Data.Models;
using Task1Project.Data.Repositories.InMemory;
using Task1Project.Logic.Services;

namespace Task1Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Repository örnekleri
            var userRepo = new InMemoryUserRepository();
            var bookRepo = new InMemoryBookRepository();
            var loanRepo = new InMemoryLoanRepository();
            var orderRepo = new InMemoryOrderRepository();
            var eventRepo = new InMemoryEventRepository();

            // Servis örnekleri
            var bookService = new BookService(bookRepo);
            var loanService = new LoanService(userRepo, bookRepo, loanRepo, eventRepo);
            var orderService = new OrderService(userRepo, bookRepo, orderRepo, eventRepo);

            userRepo.GenerateSampleUsers();
            bookRepo.GenerateSampleCatalog();

            while (true)
            {
                ShowMenu();
                Console.Write("Your choice: ");
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowCatalog(bookService);
                            break;
                        case "2":
                            AddNewBook(bookRepo);
                            break;
                        case "3":
                            AddNewUser(userRepo);
                            break;
                        case "4":
                            PlaceOrder(orderService);
                            break;
                        case "5":
                            BorrowBook(loanService);
                            break;
                        case "6":
                            ReturnBook(loanService);
                            break;
                        case "7":
                            ViewActiveLoans(loanService);
                            break;
                        case "8":
                            ViewEventLog(eventRepo);
                            break;
                        case "9":
                            ViewUsers(userRepo);
                            break;
                        case "0":
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. View catalog");
            Console.WriteLine("2. Add new book");
            Console.WriteLine("3. Add new user");
            Console.WriteLine("4. Place an order");
            Console.WriteLine("5. Borrow a book");
            Console.WriteLine("6. Return a book");
            Console.WriteLine("7. View active loans");
            Console.WriteLine("8. View event log");
            Console.WriteLine("9. View users");
            Console.WriteLine("0. Exit");
        }

        static void ShowCatalog(BookService service)
        {
            foreach (var book in service.BrowseBooks())
                Console.WriteLine($"- {book.Id}: {book.Title} by {book.Author}");
        }

        static void AddNewBook(InMemoryBookRepository repo)
        {
            Console.Write("Book title: ");
            string title = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();

            int newBookId = GenerateNewId(repo.GetBooks().Select(b => b.Id));
            repo.AddBook(new Book { Id = newBookId, Title = title, Author = author });
            Console.WriteLine($"Book added. Book ID: {newBookId}");
        }

        static void AddNewUser(InMemoryUserRepository repo)
        {
            Console.Write("User name: ");
            string name = Console.ReadLine();

            int newUserId = GenerateNewId(repo.GetUsers().Select(u => u.Id));
            repo.AddUser(new User { Id = newUserId, Name = name });
            Console.WriteLine($"User added. User ID: {newUserId}");
        }

        static void PlaceOrder(OrderService service)
        {
            Console.Write("User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userId)) throw new Exception("Invalid user ID");

            Console.Write("Book IDs (comma-separated): ");
            var bookIds = Console.ReadLine().Split(',').Select(id => int.Parse(id.Trim())).ToList();

            service.PlaceOrder(userId, bookIds);
            Console.WriteLine("Order placed.");
        }

        static void BorrowBook(LoanService service)
        {
            Console.Write("User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userId)) throw new Exception("Invalid user ID");

            Console.Write("Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId)) throw new Exception("Invalid book ID");

            service.BorrowBook(userId, bookId);
            Console.WriteLine("Book borrowed.");
        }

        static void ReturnBook(LoanService service)
        {
            Console.Write("User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userId)) throw new Exception("Invalid user ID");

            Console.Write("Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId)) throw new Exception("Invalid book ID");

            service.ReturnBook(userId, bookId);
            Console.WriteLine("Book returned.");
        }

        static void ViewActiveLoans(LoanService service)
        {
            foreach (var loan in service.GetActiveLoans())
                Console.WriteLine($"Loan: User {loan.UserId} -> Book {loan.BookId}");
        }

        static void ViewEventLog(InMemoryEventRepository repo)
        {
            foreach (var evt in repo.GetEvents())
                Console.WriteLine($"{evt.Timestamp}: {evt.Description}");
        }

        static void ViewUsers(InMemoryUserRepository repo)
        {
            foreach (var user in repo.GetUsers())
                Console.WriteLine($"User ID: {user.Id}, Name: {user.Name}");
        }

        static int GenerateNewId(IEnumerable<int> existingIds)
        {
            return existingIds.Any() ? existingIds.Max() + 1 : 1;
        }
    }
}
