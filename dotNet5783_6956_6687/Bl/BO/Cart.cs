namespace BO;
internal class Cart
{/// <summary>
/// the customers name
/// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// the customers email
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// Customer Address in cart BO
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// list of order items BO-cart
    /// </summary>
    public IEnumerable<OrderItem>? Items { get; set; }  
    /// <summary>
    /// total price of cart BO-Cart
    /// </summary>
    public double? TotalPrice { get; set; }
    /// <summary>
    /// tostring BO-Cart
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
Customers name: {CustomerName}
Customers email:{CustomerEmail}
Customers address:{CustomerAddress} 
Items: {Items}
Total price: {TotalPrice}
";
}

