using System.Data;
using System.Data.OleDb;
namespace DTOLibrary
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

        int Update(string sql);

        IDataReader Select(string sql);




    }
}
