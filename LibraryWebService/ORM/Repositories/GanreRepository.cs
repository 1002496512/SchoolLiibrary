using DTOLibrary;
using System.Data;

namespace LibraryWebService
{
    public class GanreRepository : Repository, IRepository<Ganre>
    {
        public GanreRepository(DbContext DbContext,
                             UOWModelCreators UOWModelCreators) :
                             base(DbContext, UOWModelCreators)
        {

        }
        public bool Create(Ganre item)
        {
            string sql = $@"INSERT INTO Ganres
                            (GanreId) VALUES (@GanreName)";
            this.dbContext.AddParameter("@GanreName", item.GanreName);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"Delete from Ganres where GanreId=@GanreId";
            this.dbContext.AddParameter("@GanreId", id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Ganre> GetAll()
        {
            List<Ganre> ganres = new List<Ganre>();
            string sql = "SELECT * FROM Genres";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    ganres.Add(this.UOWModelCreators.GanreCreator.CreateModel(reader));
                }
            }
            return ganres;
        }

        public Ganre GetById(string id)
        {
            string sql = "SELECT * FROM Ganres where GanreId=@GanreId";
            this.dbContext.AddParameter("@GanreId", id);

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.UOWModelCreators.GanreCreator.CreateModel(reader);
            }
        }

        public bool Update(Ganre item)
        {
            string sql = $@"Update Ganres set GanreName=@GanreName)";
            this.dbContext.AddParameter("@GanreName", item.GanreName);
            return this.dbContext.Insert(sql) > 0;
        }

        internal List<Ganre> GetGanresByBook(string bookId)
        {
            List<Ganre> ganres = new List<Ganre>();
            string sql = @"SELECT Genres.GenreName, Genres.GenreId, BooksGenres.BookId
                           FROM Genres INNER JOIN BooksGenres ON Genres.GenreId = BooksGenres.GenreId
                           WHERE BooksGenres.BookId=@BookId";
            this.dbContext.AddParameter("@BookId", bookId);

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    ganres.Add(this.UOWModelCreators.GanreCreator.CreateModel(reader));
                }
            }
            return ganres;
        }
    }
}
