using System.Collections.Generic;
using Task1Project.Data.Models;
using Task1Project.Data.Abstract;

namespace Task1Project.Data.Repositories.InMemory
{
    public class InMemoryEventRepository : AbstractEventRepository
    {
        private readonly List<Event> events = new();

        public override List<Event> GetEvents() => events;

        public override void AddEvent(Event evt) => events.Add(evt);
    }
}
