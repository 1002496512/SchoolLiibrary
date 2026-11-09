using DTOLibrary;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace LibraryWebService
{
    public class BookRepository : Repository, IRepository<Book>
    {
        int allBooks;

        public int AllBooks
        {
            get { return this.allBooks; }
        }
        public BookRepository(DbContext DbContext,
                              UOWModelCreators UOWModelCreators) :
                              base(DbContext, UOWModelCreators)
        {

        }

        public bool Create(Book model)
        {
            string sql = $@"INSERT INTO Books
                            (
                              BookName, 
                              BookDescription, 
                              BookImage,
                            )
                           VALUES
                           (
                               @BookName,  @BookDescription,
                               @BookImage
                           )";
            this.dbContext.AddParameter("@BookName", model.BookName);
            this.dbContext.AddParameter("@BookDescription", model.BookDescription);
            this.dbContext.AddParameter("@BookImage", model.BookImage);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"delete from Books where BookId=@BookId";
            this.dbContext.AddParameter("@BookId", id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Book> GetAll()
        {

            string sql = "SELECT * FROM Books";
            return GetBookList(sql);
        }

        private List<Book> GetBookList(string sql)
        {
            List<Book> books = new List<Book>();
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    books.Add(this.UOWModelCreators.BookCreator.CreateModel(reader));
                }
            }
            return books;
        }


        public List<Book> GetBooksbyGanre(string ganreId)
        {
            string sql = @"SELECT Books.BookId, Books.BookName, Books.BookDescription, Books.BookImage, Books.BookCopies, BooksGenres.GenreId
                          FROM Books INNER JOIN BooksGenres ON Books.BookId = BooksGenres.BookId
                          WHERE BooksGenres.GenreId=@GanreId;";
            this.dbContext.AddParameter("@GanreId", ganreId);
            return GetBookList(sql);
        }
        public List<Book> GetBooksbyAuthor(string AuthorId)
        {
            string sql = @"SELECT Books.BookId, Books.BookName, Books.BookDescription, Books.BookImage, Books.BookCopies, BooksAuthors.AuthorId
                           FROM Books INNER JOIN BooksAuthors ON Books.BookId = BooksAuthors.BookId
                           WHERE (((BooksAuthors.AuthorId)=@AuthorId))";
            this.dbContext.AddParameter("@AuthorId", AuthorId);
            return GetBookList(sql);
        }


        public Book GetById(string id)
        {
            string sql = $"SELECT * FROM Books where Bookid=@Bookid";
            this.dbContext.AddParameter("@Bookid", id);

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.UOWModelCreators.BookCreator.CreateModel(reader);
            }
        }

        public bool Update(Book item)
        {
            string sql = $@"Update set Books
                              BookName=@BookName, 
                              BookDescription=@BookName, 
                              BookImage=@BookName,
                              BookCopies=@BookName
                              where BookId=@BookId";
            this.dbContext.AddParameter("@BookName", item.BookName);
            this.dbContext.AddParameter("@BookDescription", item.BookDescription);
            this.dbContext.AddParameter("@BookImage", item.BookImage);
            this.dbContext.AddParameter("@BookId", item.BookId);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Book> GetBooksByGanre(string ganreId)
        {
            string sql = @"SELECT Books.BookId, Books.BookName, Books.BookDescription, Books.BookImage, Books.BookCopies, BooksGenres.TypeBookId
                           FROM Books INNER JOIN BooksGenres ON Books.BookId = BooksGenres.BookId
                           WHERE  BooksGenres.TypeBookId=@GanreId "; ;
            this.dbContext.AddParameter("@GanreId", ganreId);

            List<Book> books = new List<Book>();
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    books.Add(this.UOWModelCreators.BookCreator.CreateModel(reader));
                }
            }
            return books;
        }
        public List<Book> GetBooksByAuthor(string authorId)
        {
            string sql = @"SELECT Books.BookId, Books.BookName, Books.BookDescription, Books.BookImage, Books.BookCopies, BooksAuthors.AuthorId
                          FROM Books INNER JOIN BooksAuthors ON Books.BookId = BooksAuthors.BookId
                          WHERE (((BooksAuthors.AuthorId)=@AuthorId));";
            this.dbContext.AddParameter("@AuthorId", authorId);

            List<Book> books = new List<Book>();
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    books.Add(this.UOWModelCreators.BookCreator.CreateModel(reader));
                }
            }
            return books;
        }

        public List<Book> GetBooksByPage(int page)
        {
            int booksperPage = 10;
            List<Book> books = this.GetAll();
            return books.Skip(booksperPage * (page - 1)).Take(booksperPage).ToList();


        }

        public bool AddBookAuthor(string bookId, string authorid)
        {
            string sql = $@"INSERT INTO BooksAuthors
                            (
                              BookId, 
                              AuthorId, 
                            )
                           VALUES
                           (
                               @BookId,  @AuthorId,
                           )";
            this.dbContext.AddParameter("@BookId", bookId);
            this.dbContext.AddParameter("@AuthorId", authorid);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool AdBookGanre(string bookId, string ganreId)
        {
            string sql = $@"INSERT INTO BooksGenres
                            (
                              BookId, 
                              GanreId, 
                            )
                           VALUES
                           (
                               @BookId,  @GanreId,
                           )";
            this.dbContext.AddParameter("@BookId", bookId);
            this.dbContext.AddParameter("@GanreId", ganreId);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool DeleteBookAuthor(string bookId, string authorid)
        {
            string sql = $@"delete from BooksAuthors where BookId=@BookId And AuthorId=@AuthorId";
            this.dbContext.AddParameter("@BookId", bookId);
            this.dbContext.AddParameter("@AuthorId", authorid);
            return this.dbContext.Insert(sql) > 0;
        }

        internal bool DeleteBookGanre(string bookId, string ganreId)
        {
            string sql = $@"delete from BooksGenres where BookId=@BookId And GanreId=@GanreId";
            this.dbContext.AddParameter("@BookId", bookId);
            this.dbContext.AddParameter("@GanreId", ganreId);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool GeleteBookAuthors(string bookId)
        {
            string sql = $@"delete from BooksAuthors where BookId=@BookId";
            this.dbContext.AddParameter("@BookId", bookId);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool DeleteBookGanres(string bookId)
        {
            string sql = $@"delete from BooksGenres where BookId=@BookId";
            this.dbContext.AddParameter("@BookId", bookId);
            return this.dbContext.Insert(sql) > 0;
        }

        public int AllBookCount()
        {
            string sql = $@"SELECT Count(Books.BookId) AS BookCount FROM Books";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return Convert.ToInt32(reader["BookCount"]);
            }
        }

        internal List<Book> GetBokByPage(int page, int booksPerPage)
        {
            List<Book> books = this.GetAll();
            return GetBooksForPage(books, page, booksPerPage);
        }

        internal List<Book> GetBooksByGanre(int page, string ganreId, int booksPerPage)
        {
            string sql = @$"SELECT Books.BookId, Books.BookName, Books.BookDescription,
                            Books.BookImage, Books.BookCopies, BooksGenres.GenreId
                            FROM Books INNER JOIN BooksGenres ON Books.BookId = BooksGenres.BookId
                            WHERE BooksGenres.GenreId={ganreId}";

            List<Book> books = GetBookList(sql);
            return GetBooksForPage(books,page, booksPerPage);
        }


        private List<Book> GetBooksForPage(List<Book> books,int page, int booksPerPage)
        {
            this.allBooks = books.Count;
            return books.Skip<Book>((page - 1) * booksPerPage).Take(booksPerPage).ToList<Book>();
        }

        internal List<Book> GetBooksByAuthor(int page, string authorId, int booksPerPage)
        {
            string sql = @$"SELECT Books.BookId, Books.BookName, Books.BookDescription,
                            Books.BookImage, Books.BookCopies, BooksAuthors.AuthorId
                            FROM Books INNER JOIN BooksAuthors ON Books.BookId = BooksAuthors.BookId
                            WHERE BooksAuthors.AuthorId={authorId};
";

            List<Book> books = GetBookList(sql);
            return GetBooksForPage(books, page, booksPerPage);
        }
    }
}

