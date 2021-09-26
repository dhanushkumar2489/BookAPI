using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class BookController : ApiController
    {
        private IBookRepository repo;

        public BookController()
        {
            this.repo = new BookSQLImp();
        }

        [HttpGet]
        public List<Book> Get()
        {
            return repo.GetAllBooks();
        }

        [HttpGet]
        public Book Get(int id)
        {
            return repo.GetBookById(id);
        }

        [HttpPost]
        public Book Post(Book book)
        {
            return repo.AddBook(book);
        }

        [HttpPut]
        public Book Put(int id,Book book)
        {
            return repo.UpdateBook(id, book);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            repo.DeleteBook(id);
        }
    }
}
