using DO;
namespace DalApi;

public interface IOrder:ICrud<Order?>
{
    public IEnumerable<OrderItem?> GetAll(int id);
}
