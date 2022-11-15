namespace DalApi;

public interface IDal
{
    public IOrder order { get; }
    public IOrderItem orderItem { get; }
    public IProduct product { get; }
}
