
namespace BO;

public class OrderItem  //?????
{
    /// <summary>
    /// orter item id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the product id
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// the name of the orderitem
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// the price of product
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// the amount od the product
    /// </summary>
    public int? Amount { get; set; }
    /// <summary>
    /// the total amount of orderItem
    /// </summary>
    public double? TotalPrice { get; set; }
    /// <summary>
    /// to string to print all details
    /// </summary>
    /// <returns></returns>
 public override string ToString() => $@"
Order Item ID: {ID}
Name: {Name}
Product ID: {ProductID}
Price: {Price}
Amount: {Amount}
Total price: {TotalPrice}

    ";
}
