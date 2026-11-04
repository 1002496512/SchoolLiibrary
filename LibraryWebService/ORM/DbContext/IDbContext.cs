using System.Data;
using System.Data.OleDb;
namespace LibraryWebService
{
    public interface IDbContext
    {
        void OpenConnection();
        void CloseConnection();
        void BeginTransaction();
        void Commit();
        void RollBack();
        int Delete(string sql);
        int Insert(string sql);

        int ChangeDb(string sql);
        IDataReader Select(string sql);




    }
}
