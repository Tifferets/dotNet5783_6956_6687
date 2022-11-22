namespace BO;

internal class ProductItem
{
    /// <summary>
    /// the product items name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// the product items Id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the product items price
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the category of the product item
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// the amount of product items 
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// true if the product item is in stock
    /// </summary>
    public bool Instock{get;set;}
public override string ToString() => $@"
Name: {Name}
ID:{ID}
Price:{Price}
Category{Category}
Amount{Amount}
InStock?{Instock}
";
}
