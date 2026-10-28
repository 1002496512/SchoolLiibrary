using DTOLibrary;
using System.Data;
namespace LibraryWebService
{
    public class BookCreator : IModelCreator<Book>
    {
        public Book CreateModel(IDataReader reader)
        {
            return new Book()
            {
                BookDescription = Convert.ToString(reader["BookDescription"]),
                BookId = Convert.ToString(reader["BookId"]),
                BookImage = Convert.ToString(reader["BookImage"]),
                BookName = Convert.ToString(reader["BookName"]),
                BookCopies = Convert.ToString(reader["BookCopies"])
            };
        }
    }
}
