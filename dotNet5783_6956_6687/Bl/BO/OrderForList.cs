using System.Diagnostics;
using System.Xml.Linq;

namespace BO;

public class OrderForList
{
    /// <summary>
    /// the customers name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// the Orderforlists id name
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the status of the Orderforlists
    /// </summary>
    public OrderStatus? Status { get; set; }
    /// <summary>
    /// the amount of items in the Orderforlists
    /// </summary>
    public int? AmountOfItems { get; set; }
    /// <summary>
    /// the total price of the Orderforlists
    /// </summary>
    public double? TotalPrice { get; set; }


    public override string ToString() => $@"
Name: {CustomerName}
ID:{ID}
Status: {Status}
Amount of items:{AmountOfItems}
Total price:{TotalPrice}
";
}
