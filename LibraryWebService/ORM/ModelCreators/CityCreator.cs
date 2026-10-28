using DTOLibrary;
using System.Data;

namespace LibraryWebService
{
    public class CityCreator : IModelCreator<City>
    {
        public City CreateModel(IDataReader reader)
        {
            return new City
            {
                CityId = Convert.ToString(reader["CityId"]),
                CityName = Convert.ToString(reader["CityName"]),
            };
        }
    }
}
