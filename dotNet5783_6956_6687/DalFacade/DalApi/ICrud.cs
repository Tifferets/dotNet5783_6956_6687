

namespace DalApi;

public interface  ICrud<T>
{
    T Get(int id);
    void Update(T entity);
    void Delete(int id);
    int Add(T other);
    IEnumerable<T> GetAll();

}
