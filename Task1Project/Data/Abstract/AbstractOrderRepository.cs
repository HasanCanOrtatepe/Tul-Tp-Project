using System.Collections.Generic;
using Task1Project.Data.Models;

namespace Task1Project.Data.Abstract
{
    public abstract class AbstractOrderRepository
    {
        public abstract List<Order> GetOrders();
        public abstract void AddOrder(Order order);
    }
}
