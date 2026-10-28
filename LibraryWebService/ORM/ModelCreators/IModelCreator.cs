using System.Data;

namespace LibraryWebService
{
    public interface IModelCreator<T>
    {
        T CreateModel(IDataReader reader);  
    }
}
