using System.Data;

namespace LibraryWS
{
    public interface IModelCreator<T>
    {
        T CreateModel(IDataReader reader);  
    }
}
