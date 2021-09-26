using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        Book GetBookById(int id);
        Book AddBook(Book book);
        Book UpdateBook(int id, Book book);
        void DeleteBook(int id);
    }
}