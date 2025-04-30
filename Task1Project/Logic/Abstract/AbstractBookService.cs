using System.Collections.Generic;
using Task1Project.Data.Models;

namespace Task1Project.Logic.Abstract
{
    public abstract class AbstractBookService
    {
        public abstract List<Book> BrowseBooks();
    }
}
