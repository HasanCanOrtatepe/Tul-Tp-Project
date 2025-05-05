using System.Collections.Generic;

namespace Task1Project.Data.Models.abstracts
{
    public abstract class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<int> BookIds { get; set; }
    }
}
