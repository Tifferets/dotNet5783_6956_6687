using DO;

namespace BO;
public class Product
{
    public int Id { get; set; } 
    public string? Name { get; set; }    
    public int? Price { get; set; }    
    public int? InStock { get; set; }    
    public Category? Category { get; set; }
    public override string ToString() => $@"
Product ID: {Id}
Name: {Name}
category : {Category}
Price: {Price}
Amount in stock: {InStock}
";
}


