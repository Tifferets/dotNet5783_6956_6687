namespace DO;
/// <summary>
/// structure for orders
/// </summary>
public struct Order
{  
    /// <summary>
    /// orders id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the customers name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// customers email 
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// customers address 
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// the date the order purchested
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// the date the order was shipped
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// the date the order was delivered
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// to string to print all details
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
Order ID: {ID}
Customers name: {CustomerName}
Customers email:{CustomerEmail}
Customers address:{CustomerAddress}
Order date: {OrderDate}
Shipping date: {ShipDate}
Delivery date; {DeliveryDate}
";

}
