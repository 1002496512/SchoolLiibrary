using DTOLibrary;
using System.Data;

namespace LibraryWebService
{
    public class GanreCreator:IModelCreator<Ganre>
    {
        public Ganre CreateModel(IDataReader reader)
        {
            return new Ganre()
            {
                 GanreId= Convert.ToString(reader["GenreId"]),
                GanreName=Convert.ToString(reader["GenreName"])
            };
        }

        
    }
}
