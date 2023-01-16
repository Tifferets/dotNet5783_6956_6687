using DalApi;
namespace Dal;

sealed internal class DalList : IDal
{
    private DalList() { }
    public IOrder order { get; } = new Dal.DalOrder();
    public IOrderItem orderItem { get; } = new Dal.DalOrderItem();
    public IProduct product { get; } = new Dal.DalProduct();
    public static IDal Instance { get; } = new DalList();
}
