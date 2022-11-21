using DO;

namespace BO;
public class Product
{
/// <summary>
/// products id
/// </summary>
    public int Id { get; set; } 
    /// <summary>
    /// prodicts name
    /// </summary>
    public string? Name { get; set; }  
    /// <summary>
    /// products price
    /// </summary>
    public int? Price { get; set; }  
    /// <summary>
    /// the amount of products in stock
    /// </summary>
    public int? InStock { get; set; } 
    /// <summary>
    /// the category the product belongs to
    /// </summary>
    public Category? Category { get; set; }

 public override string ToString() => $@"
Product ID: {Id}
Name: {Name}
category : {Category}
Price: {Price}
Amount in stock: {InStock}
";
}


