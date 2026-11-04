using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace LibraryWebService
{
    public interface IRepository<T>
    {
        bool Delete(string id);
        bool Insert(T model); // create

        bool Update(T model);

        T Select(string id);

        List<T> SelectAll();
    }
}
