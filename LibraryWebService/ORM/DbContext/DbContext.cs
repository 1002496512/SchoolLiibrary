using System.Data;
using System.Data.OleDb;

namespace LibraryWebService
{
    public class DbContext : IDbContext
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbTransaction transaction;

        public DbContext()
        {
            this.connection = new OleDbConnection();
            this.connection.ConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\AlexLessons\2026-2027\YB\SchoolLiibrary\LibraryWebService\AppData\Library.accdb;Persist Security Info=True";
            this.command = new OleDbCommand();
            this.command.Connection = this.connection;
        }
        public void BeginTransaction()
        {
            this.transaction = this.connection.BeginTransaction();
        }

        public void CloseConnection()
        {
            this.connection.Close();
        }

        public void Commit()
        {
            this.transaction.Commit();
        }

        public int Delete(string sql)
        {
            return ChangeDb(sql);
        }

        public int Insert(string sql)
        {
            return ChangeDb(sql);
        }

        public void OpenConnection()
        {
            this.connection.Open();
        }

        public void RollBack()
        {
            this.transaction.Rollback();
        }

        public IDataReader Select(string sql)
        {
            this.command.CommandText = sql;
            IDataReader dataReader= this.command.ExecuteReader();
            this.command.Parameters.Clear();
            return dataReader;  
        }

       

        public int ChangeDb(string sql)
        {
            this.command.CommandText = sql;
            int records = this.command.ExecuteNonQuery();
            this.command.Parameters.Clear();
            return records;
        }
        public void AddParameter(string name, object value)
        {
            this.command.Parameters.Add(new OleDbParameter(name, value));
        }
        public object GetValue(string sql)
        {
            this.command.CommandText = sql;
            object value = this.command.ExecuteScalar();
            this.command.Parameters.Clear();
            return value;
        }

        internal object ExecuteScalar(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
