using DTOLibrary;
using System.Data;

namespace LibraryWebService
{
    public class CountryRepository : Repository, IRepository<Country>
    {
        public CountryRepository(DbContext DbContext,
                               UOWModelCreators UOWModelCreators) :
                               base(DbContext, UOWModelCreators)
        {

        }

        public bool Create(Country item)
        {
            string sql = $@"INSERT INTO Coutries
                            (CountryId) VALUES (@CountryName)";
            this.dbContext.AddParameter("@CountryName", item.CountryName);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"Delete from Countries where CountryId=@CountryId";
            this.dbContext.AddParameter("@CountryId", id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Country> GetAll()
        {
            List<Country> countries = new List<Country>();
            string sql = "SELECT * FROM Countries";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    countries.Add(this.UOWModelCreators.CountryCreator.CreateModel(reader));
                }
            }
            return countries;
        }

        public Country GetById(string id)
        {
            string sql = "SELECT * FROM Countries where CountryId=@Countryid";
            this.dbContext.AddParameter("@CountryId", id);
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.UOWModelCreators.CountryCreator.CreateModel(reader);
            }
        }

        public bool Update(Country item)
        {
            string sql = $@"Update Countries set CountryName=@CountryName)";
            this.dbContext.AddParameter("@CountryName", item.CountryName);
            return this.dbContext.Insert(sql) > 0;
        }
    }
}
