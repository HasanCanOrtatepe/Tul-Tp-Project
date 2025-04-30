using System.Collections.Generic;
using Task1Project.Data.Models;
using Task1Project.Data.Abstract;

namespace Task1Project.Data.Repositories.InMemory
{
    public class InMemoryOrderRepository : AbstractOrderRepository
    {
        private readonly List<Order> orders = new();

        public override List<Order> GetOrders() => orders;

        public override void AddOrder(Order order) => orders.Add(order);
    }
}
