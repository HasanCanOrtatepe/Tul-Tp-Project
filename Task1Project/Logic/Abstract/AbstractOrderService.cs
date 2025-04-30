using System.Collections.Generic;

namespace Task1Project.Logic.Abstract
{
    public abstract class AbstractOrderService
    {
        public abstract void PlaceOrder(int userId, List<int> bookIds);
    }
}
