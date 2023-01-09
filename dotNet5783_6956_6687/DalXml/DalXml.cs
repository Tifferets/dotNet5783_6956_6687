using DalApi;
namespace Dal;

 internal sealed class DalXml:IDal
{
    private DalXml() { }
    public IProduct product { get; } = new Dal.DoProduct();
    public IOrder order { get; } = new Dal.DoOrder();
    public IOrderItem orderItem { get; } = new Dal.DoOrderItem();
}
