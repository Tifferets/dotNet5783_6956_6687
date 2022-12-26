using System.Diagnostics;
using System.Xml.Linq;
namespace BO;
public class OrderTracking//missing something
{
    public int ID { get; set; }
    /// <summary>
    /// the orders status
    /// </summary>
    public OrderStatus Status { get; set; }
    public List<Tuple<OrderStatus,DateTime>>? tracking { get; set; }    
    public override string ToString() => $@"
ID:{ID}
Tracking Information :
{string.Join("\n" , tracking ?? throw new BO.NullException())}

";
   //Status:{Status}
}


