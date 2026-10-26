using DTOLibrary;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWS
{
    public class ReaderCreator : IModelCreator<Reader>
    {
        public Reader CreateModel(IDataReader reader)
        {
            return new Reader
            {
                CityId = reader["CityId"].ToString(),
                ReaderAdress = reader["ReaderAdress"].ToString(),
                ReaderFirstName = reader["ReaderFirstName"].ToString(),
                ReaderId = reader["ReaderId"].ToString(),
                ReaderImage = reader["ReaderImage"].ToString(),
                ReaderLastName = reader["ReaderLastName"].ToString(),
                ReaderNickName = reader["ReaderNickName"].ToString(),
                ReaderPassword = reader["ReaderPassword"].ToString(),
                ReaderTelephone = reader["ReaderTelephone"].ToString(),
                Salt = reader["ReaderSalt"].ToString()
            };
        }
    }
}
