using System.Data;
using DTOLibrary;

namespace LibraryWebService.ORM
{
    public class AuthorCreator : IModelCreator<Author>
    {
        public Author CreateModel(IDataReader dataReader)
        {
            Author author = new Author()
            {
                AuthorFirstName = Convert.ToString(dataReader["AuthorFirstName"]),
                AuthorId = Convert.ToString(dataReader["AuthorId"]),
                AuthorLastName = Convert.ToString(dataReader["AuthorLastName"]),
                AuthorPicture = Convert.ToString(dataReader["AuthorLastName"]),
                AuthorYear = Convert.ToInt16(dataReader["AuthorLastName"]),
                CountryId = Convert.ToInt16(dataReader["AuthorLastName"])
            };
            return author;
        }
    }
}
