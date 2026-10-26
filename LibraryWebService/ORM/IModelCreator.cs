using System.Data;

namespace LibraryWebService.ORM
{
    public interface IModelCreator<T>
    {
        T CreateModel(IDataReader dataReader);
    }
}
