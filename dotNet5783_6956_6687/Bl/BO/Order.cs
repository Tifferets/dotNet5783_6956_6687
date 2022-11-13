

namespace BO;

public class Order
{
    public int ID { get; set; } 
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; } 
    public DateTime OrderDate { get; set; } 
    public DateTime ShipDate { get; set; } 
    public DateTime DeliveryDate { get; set; }

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
