using DTOLibrary;
using LibraryWebService;
using System.Collections.Generic;
using System.Data;

namespace LibraryWebService
{
    public class AuthoRepository : Repository, IRepository<Author>
    {
        public AuthoRepository(DbContext dbContext, UOWModelCreators UOWModelCreators)
            :base(dbContext, UOWModelCreators)
        {

        }
        public bool Create(Author model)
        {
            //string sql = @$"Insert into Authors
            //                (
            //                  AuthorFirstName, AuthorLastName,
            //                  AuthorYear,CountryId,AuthorPicture
            //                )
            //                values
            //                (
            //                     '{model.AuthorFirstName}','{model.AuthorLastName}',
            //                     {model.AuthorYear}, {model.CountryId},'{model.AuthorPicture}'
            //                )";
            string sql = @$"Insert into Authors
                            (
                              AuthorFirstName, AuthorLastName,
                              AuthorYear,CountryId,AuthorPicture
                            )
                            values
                            (
                                 @AuthorFirstName,@AuthorLastName,
                                 @AuthorYear, @CountryId,@AuthorPicture
                            )";
            this.dbContext.AddParameter("@AuthorFirstName", model.AuthorFirstName);
            this.dbContext.AddParameter("@AuthorLastName", model.AuthorLastName);
            this.dbContext.AddParameter("@AuthorYear", model.AuthorYear.ToString());
            this.dbContext.AddParameter("@CountryId", model.CountryId.ToString());
            this.dbContext.AddParameter("@AuthorPicture", model.AuthorPicture);
            return this.dbContext.Insert(sql)>0;
        }
        public bool Delete(string id)
        {
            string sql = @"Delete from Authors where authorId=@authorId";
            this.dbContext.AddParameter("@authorId", id);
            return this.dbContext.Delete(sql) > 0;
        }

        public List<Author> GetAll()
        {
            string sql = "Select * from Authors";
            
            List<Author> authors = new List<Author>();
            using(IDataReader reader = this.dbContext.Select(sql))
            {
               while (reader.Read())
               {
                authors.Add(this.UOWModelCreators.AuthorCreator.CreateModel(reader));
               }
            }
           
            return authors;
        }

        public Author GetById(string id)
        {
            string sql = "Select * from Authors where AuthorId=@AuthorId";
            this.dbContext.AddParameter("@authorId", id);
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.UOWModelCreators.AuthorCreator.CreateModel(reader);
            }
        }

        public bool Update(Author model)
        {
            string sql = @"Update Authors set
                                             AuthorFirstName=@AuthorFirstName,
                                             AuthorLastName=@AuthorLastName.
                                             AuthorYear=@AuthorYear,
                                             CountryId=@CountryId,
                                             AuthorPicture=@AuthorPicture";
            this.dbContext.AddParameter("@AuthorFirstName", model.AuthorFirstName);
            this.dbContext.AddParameter("@AuthorLastName", model.AuthorLastName);
            this.dbContext.AddParameter("@AuthorYear", model.AuthorYear.ToString());
            this.dbContext.AddParameter("@CountryId", model.CountryId.ToString());
            this.dbContext.AddParameter("@AuthorPicture", model.AuthorPicture);
            return this.dbContext.ChangeDb(sql) > 0;

        }
    }
}
