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
    /// the name of the item
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// the price of the item
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// the category the item belongs in
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// the amount of 1 item
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// true if it exists in the stock,false if it doesnt exist
    /// </summary>
    public bool InStock { get; set; }

public override string ToString() => $@"
ID: {ID}
Name: {Name}
Price: {Price}
Category: {Category}
Amount: {Amount}
In stock? {InStock}
";
}

/* <Label x:Name="Name_Label" Content="Name:" HorizontalAlignment="Left" Margin="205,266,0,0" VerticalAlignment="Top" Height="34" Width="42"/>
    <Label x:Name="Category_Label" Content="Category:" HorizontalAlignment="Left" Margin="205,206,0,0"  VerticalAlignment="Top" Height="34" Width="84" RenderTransformOrigin="4.79,3.53"/>
    <Label x:Name="Price_Label" Content="Price:" HorizontalAlignment="Left" Margin="205,314,0,0" VerticalAlignment="Top" Height="34" Width="42"/>
    <Label x:Name="InStock_Label" Content="In Stock:" HorizontalAlignment="Left" Margin="205,362,0,0"  VerticalAlignment="Top" Height="34" Width="84"/>

    <TextBox x:Name="Id_Textbox" HorizontalAlignment="Left" Margin="390,161,0,0"  TextWrapping="Wrap" VerticalAlignment="Top" Width="120"/>
    <TextBox x:Name="Name_Textbox" HorizontalAlignment="Left" Margin="390,266,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="120"/>
    <TextBox x:Name="Price_Textbox" HorizontalAlignment="Left" Margin="390,322,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="120"/>
    <TextBox x:Name="InStock_Textbox" HorizontalAlignment="Left" Margin="390,370,0,0"  TextWrapping="Wrap" VerticalAlignment="Top" Width="120"/>
    <ComboBox x:Name="Category_ComboBox" HorizontalAlignment="Left" Margin="390,201,0,0"  VerticalAlignment="Top" Width="120"/>
*/