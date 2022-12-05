namespace DO;
/// <summary>
/// structure for products
/// </summary>
public struct Product
{
    /// <summary>
    /// the products name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// the products id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the price of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the category of the product -we chenged from category to string
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// amount of products in stock
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// to string to print all details
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
Product ID: {ID}
Name: {Name}
category : {Category}
Price: {Price}
Amount in stock: {InStock}
";
}
