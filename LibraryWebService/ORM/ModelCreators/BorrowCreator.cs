using DTOLibrary;
using System.Data;

namespace LibraryWebService
{
    public class BorrowCreator : IModelCreator<Borrow>
    {
        public Borrow CreateModel(IDataReader reader)
        {
            return new Borrow()
            {
                BookId = Convert.ToString(reader["BookId"]),
                BorrowDate = Convert.ToString(reader["BorrowDate"]),
                BorrowId = Convert.ToString(reader["BorrowId"]),
                BorrowStatus = Convert.ToString(reader["BorrowStatus"]),
                ReaderId = Convert.ToString(reader["ReaderId"]),
            };
        }
    }
}
