

namespace BO;

public class Order
{
    public int Id { get; set; } 
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; } 
    public DateTime OrderDate { get; set; } 
    public DateTime OrderShip { get; set; } 
    public DateTime OrderDelivery { get; set; }

}
