

using DO;

namespace DalApi;

public interface ICrud<T> /*where T: struct*/
{
    //T? Get(int id);
    void Update(T entity);
    void Delete(int id);
    int Add(T other);
    IEnumerable<T?> GetAll(Func<T?, bool>? select = null);
    T? GetSingle(Func<T?, bool>? func);
}
