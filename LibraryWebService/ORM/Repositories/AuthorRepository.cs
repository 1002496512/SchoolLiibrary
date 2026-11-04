using DTOLibrary;
using LibraryWebService;
using System.Data;
using System.Net;

namespace LibraryWebService
{
    public class AuthorRepository : Repository, IRepository<Author>
    {
        public AuthorRepository(DbContext dbContext, UOWModelCreators uowModelCreators) :
            base(dbContext, uowModelCreators)
        {
        }

        public bool Create(Author item)
        {
            //string sql = $@"INSERT INTO Authors
            //             (AuthorFirstName, AuthorLastName,
            //              AuthorLastName,AuthorYear,
            //              CountryId,AuthorPicture)
            //              VALUES ('{item.AuthorFirstName}', 
            //             '{item.AuthorLastName}',
            //              {item.AuthorYear},
            //              {item.CountryId},
            //             '{item.AuthorPicture}')";    
            string sql = $@"INSERT INTO Authors
                            (
                              AuthorFirstName, 
                              AuthorLastName, AuthorYear,
                              CountryId,AuthorPicture
                            )
                           VALUES
                           (
                               @AuthorFirstName,  @AuthorLastName,
                               @AuthorYear, @CountryId,@AuthorPicture
                           )";
            this.dbContext.AddParameter("@AuthorFirstName", item.AuthorFirstName);
            this.dbContext.AddParameter("@AuthorLastName", item.AuthorLastName);
            this.dbContext.AddParameter("@AuthorYear", item.AuthorYear);
            this.dbContext.AddParameter("@CountryId", item.CountryId);
            this.dbContext.AddParameter("@AuthorPicture", item.AuthorPicture);
            return this.dbContext.Insert(sql) > 0;  
        }

        public bool Delete(string id)
        {
            string sql = $@"DELETE FROM Authors
                           WHERE AuthorId =@authorId";
            this.dbContext.AddParameter("@authorId", id);
            return this.dbContext.Delete(sql) > 0;
        }

        public List<Author> GetAll()
        {
            List<Author> authors = new List<Author>();  
            string sql = "SELECT * FROM Authors";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while(reader.Read())
                {
                   
                    authors.Add(this.UOWModelCreators.AuthorCreator.CreateModel(reader));
                }
               
            }
            return authors;
        }

        public Author GetById(string id)
        {
            string sql = $"SELECT * FROM Authors where AuthorId=@AuthorId";
            this.dbContext.AddParameter("@AuthorId",id);
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return UOWModelCreators.AuthorCreator.CreateModel(reader);
            }
            return null;
        }

        public bool Update(Author item)
        {
            string sql = $@"Update set  Authors
                              AuthorFirstName=@AuthorFirstName, 
                              AuthorLastName=@AuthorLastName,
                              AuthorYear=@AuthorYear,
                              CountryId=@CountryId,
                              AuthorPicture=@AuthorPicture
                              where AuthorId=@AuthorId";
                        
            this.dbContext.AddParameter("@AuthorFirstName", item.AuthorFirstName);
            this.dbContext.AddParameter("@AuthorLastName", item.AuthorLastName);
            this.dbContext.AddParameter("@AuthorYear", item.AuthorYear);
            this.dbContext.AddParameter("@CountryId", item.CountryId);
            this.dbContext.AddParameter("@AuthorPicture", item.AuthorPicture);
            this.dbContext.AddParameter("@AuthorId", item.AuthorId);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Author> GetAuthorsByBook(string bookId)
        {
            List<Author> authors = new List<Author>();
            string sql = @"SELECT Authors.AuthorId, Authors.AuthorFirstName,
                                  Authors.AuthorLastName, Authors.AuthorYear,
                                  Authors.CountryId, Authors.AuthorPicture,
                                  BooksAuthors.BookId
                           FROM Authors INNER JOIN BooksAuthors ON 
                                Authors.AuthorId = BooksAuthors.AuthorId
                                WHERE BooksAuthors.BookId=@BookId";
                        this.dbContext.AddParameter("@BookId", bookId);
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {

                    authors.Add(this.UOWModelCreators.AuthorCreator.CreateModel(reader));
                }

            }
            return authors;
        }
    }
}
