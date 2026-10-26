using DTOLibrary;
using System.Data;

namespace LibraryWS
{
    public class CountryCreator : IModelCreator<Country>
    {
        public Country CreateModel(IDataReader reader)
        {
            return new Country
            {
                 CountryId = Convert.ToInt16(reader["CountryId"]),
                 CountryName = Convert.ToString(reader["CountryName"])
            };
        }
    }
}
