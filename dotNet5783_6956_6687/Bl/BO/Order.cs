namespace BO;
public class Order
{
    /// <summary>
    /// order id -BO
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Customer Name -BO
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// Customer Email BO
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// Customer Address -BO
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// Order Date -BO
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// Ship Date -BO
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// Delivery Date -BO
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// Status- from Enum OrderStatus -BO
    /// </summary>
    public OrderStatus? Status { get; set; }
    /// <summary>
    /// Payment Date -BO
    /// </summary>
    public DateTime? PaymentDate { get; set; }
    /// <summary>
    /// list of Order Item_BO
    /// </summary>
    public IEnumerable<OrderItem>? Items { get; set; }
    /// <summary>
    /// Total Price -BO
    /// </summary>
    public double? TotalPrice { get; set; }

 public override string ToString() => $@"
Order ID: {ID}
Customers name: {CustomerName}
Customers email:{CustomerEmail}
Customers address:{CustomerAddress}
Order date: {OrderDate}
Shipping date: {ShipDate}
Delivery date:{DeliveryDate}
Payment date:{PaymentDate}
Stasus:{Status}
Items:{Items}
Total price:{TotalPrice}
";
}

//namespace BO;
//public class Order
//{
//    public int ID { get; set; }
//    public string CustomerName { get; set; }
//    public string CustomerEmail { get; set; }
//    public string CustomerAddress { get; set; }
//    public DateTime OrderDate { get; set; }
//    public DateTime ShipDate { get; set; }
//    public DateTime DeliveryDate { get; set; }

//    public override string ToString() => $@"
//Order ID: {ID}
//Customers name: {CustomerName}
//Customers email:{CustomerEmail}
//Customers address:{CustomerAddress}
//Order date: {OrderDate}
//Shipping date: {ShipDate}
//Delivery date; {DeliveryDate}
//";
//}
