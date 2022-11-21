
using System.Diagnostics;
using System.Xml.Linq;

namespace BO;

internal class OrderTracking
{
    public int ID { get; set; }
    /// <summary>
    /// the orders status
    /// </summary>
    public OrderStatus Status { get; set; }
    public override string ToString() => $@"
ID:{ID}
Status:{Status}
";

}
