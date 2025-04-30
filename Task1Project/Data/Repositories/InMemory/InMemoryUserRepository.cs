using System.Collections.Generic;
using Task1Project.Data.Models;
using Task1Project.Data.Abstract;

namespace Task1Project.Data.Repositories.InMemory
{
    public class InMemoryUserRepository : AbstractUserRepository
    {
        private readonly List<User> users = new();

        public override List<User> GetUsers() => users;

        public override void AddUser(User user) => users.Add(user);

        public override void GenerateSampleUsers()
        {
            users.Add(new User { Id = 1, Name = "Alice" });
            users.Add(new User { Id = 2, Name = "Bob" });
        }
    }
}
