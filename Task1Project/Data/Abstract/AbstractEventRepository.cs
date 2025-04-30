using System.Collections.Generic;
using Task1Project.Data.Models;

namespace Task1Project.Data.Abstract
{
    public abstract class AbstractEventRepository
    {
        public abstract List<Event> GetEvents();
        public abstract void AddEvent(Event evt);
    }
}
