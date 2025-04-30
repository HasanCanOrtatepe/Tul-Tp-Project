using System;
using System.Collections.Generic;
using System.Linq;
using Task1Project.Data.Abstract;
using Task1Project.Data.Models;
using Task1Project.Logic.Abstract;

namespace Task1Project.Logic.Services
{
    public class OrderService : AbstractOrderService
    {
        private readonly AbstractUserRepository userRepo;
        private readonly AbstractBookRepository bookRepo;
        private readonly AbstractOrderRepository orderRepo;
        private readonly AbstractEventRepository eventRepo;

        public OrderService(
            AbstractUserRepository userRepo,
            AbstractBookRepository bookRepo,
            AbstractOrderRepository orderRepo,
            AbstractEventRepository eventRepo)
        {
            this.userRepo = userRepo;
            this.bookRepo = bookRepo;
            this.orderRepo = orderRepo;
            this.eventRepo = eventRepo;
        }

        public override void PlaceOrder(int userId, List<int> bookIds)
        {
            var user = userRepo.GetUsers().FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new Exception($"User with ID {userId} does not exist.");

            foreach (var bookId in bookIds)
            {
                var book = bookRepo.GetBooks().FirstOrDefault(b => b.Id == bookId);
                if (book == null)
                    throw new Exception($"Book with ID {bookId} does not exist.");
            }

            var order = new Order
            {
                Id = orderRepo.GetOrders().Count > 0 ? orderRepo.GetOrders().Max(o => o.Id) + 1 : 1,
                UserId = userId,
                BookIds = bookIds
            };

            orderRepo.AddOrder(order);
            eventRepo.AddEvent(new Event
            {
                Timestamp = DateTime.Now,
                Description = $"User {userId} placed an order for books: {string.Join(", ", bookIds)}"
            });
        }
    }
}
