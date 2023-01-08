using DalApi;
namespace Dal;

sealed internal class DalXml:IDal
{
    private DalXml() { }
    public IProduct DoProduct { get; } = new Dal.DoProduct();
    public IOrder DoOrder { get; } = new Dal.DoOrder();
    public IOrderItem DoOrderItem { get; } = new Dal.DoOrderItem();
}
