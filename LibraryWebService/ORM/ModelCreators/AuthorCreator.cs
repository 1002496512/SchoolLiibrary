using DTOLibrary;
using System.Data;

namespace LibraryWebService
{
    public class AuthorCreator : IModelCreator<Author>
    {
        public Author CreateModel(IDataReader reader)
        {
            return new Author
            {
                AuthorFirstName = Convert.ToString(reader["AuthorFirstName"]),
                AuthorId = Convert.ToString(reader["AuthorId"]),
                AuthorLastName = Convert.ToString(reader["AuthorLastName"]),
                AuthorPicture = Convert.ToString(reader["AuthorPicture"]),
                AuthorYear = Convert.ToInt16(reader["AuthorYear"]),
                CountryId = Convert.ToInt16(reader["CountryId"]),
            };
        }
    }
}
