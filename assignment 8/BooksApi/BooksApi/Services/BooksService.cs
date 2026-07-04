using BooksApi.Services.Interfaces;
using BooksApi.Models.Entities;

namespace BooksApi.Services
{
    public class BooksService : IBooksService
    {
        private List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", Year = 2008 },
            new Book { Id = 2, Title = "The Pragmatic Programmer", Author = "An Hunt", Year = 1999 },
            new Book { Id = 3, Title = "Design Patterns", Author = "Erich Gama", Year = 1994 }
        };
        public List<Book> GetAll()
        {
            return books;
        }
        public Book GetById(int id)
        {
            foreach (Book book in books)
            {
                if (book.Id == id)
                {
                    return book;
                }
            }

            return null;
        }
    }
    
}
