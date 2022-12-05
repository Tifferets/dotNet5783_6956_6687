using System.Xml.Linq;

namespace DO;
/// <summary>
/// structure for orderitems
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// orter item id
    /// </summary>
    public int OrderItemID { get; set; }
    /// <summary>
    /// the product id
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// the order id
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// the price of product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the amount od the product
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// to string to print all details
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    Order Item ID ={OrderItemID}
    Product ID={ProductID}
    Order ID={OrderID}
    Price= {Price}
    Amount={Amount}
    ";
}
