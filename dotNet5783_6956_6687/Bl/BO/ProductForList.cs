using DO;

namespace BO;
/// <summary>
/// a list of all the items
/// </summary>
public class ProductForList
{
    /// <summary>
    /// item id
    /// </summary>
    public int ID { get; set; }
   
    /// <summary>
    /// the category the item belongs in
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// the name of the item
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// the price of the item
    /// </summary>
    public double? Price { get; set; }

public override string ToString() => Extention.ToStringProperty(this);
}

