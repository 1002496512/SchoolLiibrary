using DTOLibrary;
using System.Data;
using System.Data.SqlTypes;
using System.IO.Pipelines;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;

namespace LibraryWebService
{
    public class ReaderRepository : Repository, IRepository<Reader>
    {
        public ReaderRepository(DbContext DbContext,
                              UOWModelCreators UOWModelCreators) :
                              base(DbContext, UOWModelCreators)
        {

        }
        public bool Create(Reader item)
        {
            string sql = $@"INSERT INTO Readers
                            (
                              ReaderId, 
                              ReaderFirstName, ReaderLastName,
                              ReaderAdress,ReaderTelephone,
                              ReaderImage, CityId,
                              ReaderNickName, ReaderPassword, ReaderSalt
                            )
                           VALUES
                           (
                               @ReaderId,  @ReaderFirstName,
                               @ReaderLastName, @ReaderAdress,@ReaderTelephone,
                               @ReaderImage, @CityId, 
                               @ReaderNickName, @ReaderPassword,@ReaderSalt
                           )";
            
            this.dbContext.AddParameter("@ReaderId", item.ReaderId);
            this.dbContext.AddParameter("@ReaderFirstName", item.ReaderFirstName);
            this.dbContext.AddParameter("@ReaderLastName", item.ReaderLastName);
            this.dbContext.AddParameter("@ReaderAdress", item.ReaderAdress);
            this.dbContext.AddParameter("@ReaderTelephone", item.ReaderTelephone); // הועבר למקום ה-5
            this.dbContext.AddParameter("@ReaderImage", item.ReaderImage); // הועבר למקום ה-6
            this.dbContext.AddParameter("@CityId", item.CityId); // הועבר למקום ה-7
            this.dbContext.AddParameter("@ReaderNickName", item.ReaderNickName);
            string salt = GenerateSalt();
            this.dbContext.AddParameter("@ReaderPassword", CalculateHash(item.ReaderPassword, salt));
            this.dbContext.AddParameter("@ReaderSalt", salt);

            return this.dbContext.Insert(sql) > 0;
        }

        private string CalculateHash(string password, string salt)
        {
            string s = password + salt;
            byte[] pass = System.Text.Encoding.UTF8.GetBytes(s);    
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(pass);
                return Convert.ToBase64String(bytes);
            }
        }
        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public bool Delete(string id)
        {
            string sql = $@"delete from Readers where ReaderId=@ReaderId";
            this.dbContext.AddParameter("@ReaderId", id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Reader> GetAll()
        {
            List<Reader> readers = new List<Reader>();
            string sql = "SELECT * FROM Readers";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    readers.Add(this.UOWModelCreators.ReaderCreator.CreateModel(reader));
                }
            }
            return readers;
        }

        public Reader GetById(string id)
        {
            string sql = $"SELECT * FROM Readers where Readerid=@ReaderId";
            this.dbContext.AddParameter("@ReaderId", id);
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.UOWModelCreators.ReaderCreator.CreateModel(reader);
            }
        }

        public bool Update(Reader item)
        {
            string sql = $@"INSERT set Readers
                              ReaderId=@ReaderId, 
                              ReaderFirstName=@ReaderFirstName,
                              ReaderLastName=@ReaderLastName,
                              ReaderAdress=@ReaderAdress,
                              ReaderTelephone=@ReaderTelephone,
                              ReaderImage=@ReaderImage,
                              CityId=@CityId,
                              ReaderNickName=@ReaderNickName,
                              ReaderPassword=@ReaderPassword";
            this.dbContext.AddParameter("@ReaderId", item.ReaderId);
            this.dbContext.AddParameter("@ReaderFirstName", item.ReaderFirstName);
            this.dbContext.AddParameter("@ReaderLastName", item.ReaderLastName);
            this.dbContext.AddParameter("@ReaderAdress", item.ReaderAdress);
            this.dbContext.AddParameter("@CityId", item.CityId);
            this.dbContext.AddParameter("@ReaderTelephone", item.ReaderTelephone);
            this.dbContext.AddParameter("@ReaderImage", item.ReaderImage);
            this.dbContext.AddParameter("@ReaderNickName", item.ReaderNickName);
            this.dbContext.AddParameter("@ReaderPassword", item.ReaderPassword);

            return this.dbContext.Insert(sql) > 0;
        }

        public string Login(string nickName, string password)
        {
            string sql = @"Select ReaderId,ReaderPassword,ReaderSalt from Readers 
                           where ReaderNickName=@ReaderNickName";
            this.dbContext.AddParameter("@ReaderNickName", nickName);
            string hash = string.Empty;
            string salt = string.Empty;
            string readerId = string.Empty; 
           using(IDataReader reader = this.dbContext.Select(sql))
            {
                if (reader.Read() == true)
                {
                    salt = reader["ReaderSalt"].ToString();
                    hash = reader["ReaderPassword"].ToString();
                    readerId = reader["ReaderId"].ToString();
                }
                if (hash == CalculateHash(password, salt))
                    return readerId;
                return null;

            }


        }
    }
}
