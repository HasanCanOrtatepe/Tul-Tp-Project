using System.Collections.Generic;
using Task1Project.Data.Models;

namespace Task1Project.Data.Abstract
{
    public abstract class AbstractUserRepository
    {
        public abstract List<User> GetUsers();
        public abstract void AddUser(User user);
        public abstract void GenerateSampleUsers();
    }
}
