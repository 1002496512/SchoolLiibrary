using DTOLibrary;
using System.Data;

namespace LibraryWebService
{
    public class BorrowRepository : Repository, IRepository<Borrow>
    {
        public BorrowRepository(DbContext DbContext,
                              UOWModelCreators UOWModelCreators) :
                              base(DbContext, UOWModelCreators)
        {

        }
        public bool Create(Borrow item)
        {
            string sql = $@"INSERT INTO Borrows
                            (
                              ReaderId, 
                              BorrowDate, 
                              BorrowStatus,BookId
                            )
                           VALUES
                           (
                               @ReaderId,  @BorrowDate,
                               @BorrowStatus, @BookId
                           )";
            this.dbContext.AddParameter("@ReaderId", item.ReaderId);
            this.dbContext.AddParameter("@BorrowDate", item.BorrowDate);
            this.dbContext.AddParameter("@BorrowStatus", item.BorrowStatus);
            this.dbContext.AddParameter("@BookId", item.BookId);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"delete from Borrows where BorrowId=@BorrowId";
            this.dbContext.AddParameter("@BorrowId", id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Borrow> GetAll()
        {
            List<Borrow> borrows = new List<Borrow>();
            string sql = "SELECT * FROM Borrows";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    borrows.Add(this.UOWModelCreators.BorrowCreator.CreateModel(reader));
                }
            }
            return borrows;

        }

        public Borrow GetById(string id)
        {
            string sql = $"SELECT * FROM Borrows where Borrowid=@BorrowId";
            this.dbContext.AddParameter("@BorrowId", id);
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.UOWModelCreators.BorrowCreator.CreateModel(reader);
            }
        }

        public bool Update(Borrow item)
        {
            string sql = $@"Update set Borrows
                              ReaderId=@ReaderId, 
                              BorrowDate=@BorrowDate, 
                              BorrowStatus=@BorrowStatus,
                              BookId=@BookId
                              Where BorrowId=@BorrowId";
            this.dbContext.AddParameter("@ReaderId", item.ReaderId);
            this.dbContext.AddParameter("@BorrowDate", item.BorrowDate);
            this.dbContext.AddParameter("@BorrowStatus", item.BorrowStatus);
            this.dbContext.AddParameter("@BookId", item.BookId);
            this.dbContext.AddParameter("@BorrowId", item.BorrowId);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Borrow> GetReaderBorrows(string readerId)
        {
            List<Borrow> borrows = new List<Borrow>();
            string sql = @"SELECT
                            Borrows.BorrowId,
                            Borrows.ReaderId,
                            Borrows.BorrowDate,
                            Borrows.BorrowStatus,
                            Borrows.BookId
                        FROM
                            Borrows
                        WHERE Borrows.ReaderId  = @ReaderId)
                              AND Borrows.BorrowStatus=2);";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    borrows.Add(this.UOWModelCreators.BorrowCreator.CreateModel(reader));
                }
            }
            return borrows;
        }
    }
}
