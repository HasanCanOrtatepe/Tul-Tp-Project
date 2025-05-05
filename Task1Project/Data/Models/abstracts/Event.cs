using System;

namespace Task1Project.Data.Models.abstracts
{
    public abstract class Event
    {
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
    }
}
