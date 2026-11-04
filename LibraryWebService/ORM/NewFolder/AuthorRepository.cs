using DTOLibrary;

namespace LibraryWebService
{
    public class AuthorRepository : Repository, IRepository<Author>
    {
        public AuthorRepository(DbContext dbContext,
                                UOWModelCreators modelCreators):
               base(dbContext, modelCreators)
        {


        }
        public bool Delete(string id)
        {
            string sql = $"Delete from Authors where AuthorId={id}";
            return this.dbContext.ChangeDb(sql) > 0;
        }

        public bool Insert(Author author)
        {
            //string sql = @$"Insert into Authors(AuthorFirstName,AuthorLastName, 
            //                                     AuthorYear,CountryId,AuthorPicture)
            //                                     values('{author.AuthorFirstName}', '{author.AuthorLastName}',
            //                                            {author.AuthorYear},{author.CountryId},
            //                                            '{author.AuthorPicture}')";

            string sql = @$"Insert into Authors(AuthorFirstName,AuthorLastName, 
                                                 AuthorYear,CountryId,AuthorPicture)
                                                 values(
                                                         @AuthorFirstName, @AuthorLastName,
                                                         @AuthorYear,@CountryId,
                                                         @AuthorPicture
                                                       )";
            this.dbContext.AddParameter("@AuthorFirstName", author.AuthorFirstName);
            this.dbContext.AddParameter("@AuthorLastName", author.AuthorLastName);
            this.dbContext.AddParameter("@AuthorYear", author.AuthorYear);
            this.dbContext.AddParameter("@CountryId", author.CountryId);
            this.dbContext.AddParameter("@AuthorPicture", author.AuthorPicture);

            return this.dbContext.ChangeDb(sql) > 0;
        }

        public Author Select(string id)
        {
            throw new NotImplementedException();
        }

        public List<Author> SelectAll()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
