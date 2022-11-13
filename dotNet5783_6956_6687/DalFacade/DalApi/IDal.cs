

namespace DalApi;

public interface IDal
{
    IOrder order { get; }
    IOrderItem orderItem { get; }
    IProduct product { get; }
}
