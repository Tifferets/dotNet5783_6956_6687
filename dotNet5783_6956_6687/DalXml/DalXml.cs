using DalApi;
using System.Diagnostics;

namespace Dal;
 internal sealed class DalXml:IDal
{
    private DalXml() { }
    public IProduct product { get; } = new Dal.DoProduct();
    public IOrder order { get; } = new Dal.DoOrder();
    public IOrderItem orderItem { get; } = new DoOrderItem();
    public static IDal Instance { get; } = new DalXml();
}
