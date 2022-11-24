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
    public string? CustomerAddress { get; set; }
    public IEnumerable<OrderItem>? Items { get; set; }    
    public double? TotalPrice { get; set; }
    public override string ToString() => $@"
Customers name: {CustomerName}
Customers email:{CustomerEmail}
Customers address:{CustomerAddress} 
Items: {Items}
Total price: {TotalPrice}
";
}

