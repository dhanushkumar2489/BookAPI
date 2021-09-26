using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication3.Models
{
    public class BookSQLImp : IBookRepository
    {
        public List<Book> GetAllBooks()
        {
            List<Book> books=new List<Book>();
            string connectionString = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "select * from Book";
                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read())
                {
                    Book book = new Book();
                    book.Id = dr.GetInt32(0);
                    book.Author = dr.GetString(1);
                    book.Title = dr.GetString(2);
                    book.Price = dr.GetInt32(3);
                    books.Add(book);
                }
            }
            return books;
        }

        public Book GetBookById(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            Book book = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "select * from Book where Id =@id";
                comm.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    book = new Book()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Author = dr["Author"].ToString(),
                        Title = dr["Title"].ToString(),
                        Price = Convert.ToInt32(dr["Price"])
                    };
                }
            }
            return book;
        }

        public Book AddBook(Book book)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "insert into Book(Id, Author,Title,  Price) values (@Id, @Author,@Title, @Price); SELECT SCOPE_IDENTITY()";
                comm.Parameters.AddWithValue("@Id", book.Id);
                comm.Parameters.AddWithValue("@Author", book.Author);
                comm.Parameters.AddWithValue("@Title", book.Title);
                comm.Parameters.AddWithValue("@Price", book.Price);
                conn.Open();
                int id = Convert.ToInt32(comm.ExecuteNonQuery());
            }
            return book;
        }

        public Book UpdateBook(int id, Book book)
        {
            Book oldbook = GetBookById(id);
            string connectionString = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                if (oldbook == null)
                {
                    comm.CommandText = "insert into Book( Id, Author,Title, Price) values ( @book.Id, @book.Author,@book.Title, @book.Price); SELECT SCOPE_IDENTITY()";
                }
                else
                {
                    comm.CommandText = "update Book set  Author = @author,Title = @title, Price = @price where Id = @id";
                }
                comm.Parameters.AddWithValue("@id", book.Id);
                comm.Parameters.AddWithValue("@author", book.Author);
                comm.Parameters.AddWithValue("@title", book.Title);
                comm.Parameters.AddWithValue("@price", book.Price);
                conn.Open();
                if (oldbook == null)
                {
                    id = Convert.ToInt32(comm.ExecuteScalar());
                }
                else
                {
                    int row=comm.ExecuteNonQuery();
                }
            }
            return book;
        }

        public void DeleteBook(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "delete from Book where Id= @id";
                comm.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int row = comm.ExecuteNonQuery();
            }
        }
    }
}