using DO;

namespace Dal;

public class DalProduct
{
    public int Add(Product product)
    {
        product.ID = DataSource.config.GetProductID;//gets a generated id from data source inner class
        return product.ID;
    }
    public Product Get(int productID)
    {
        //try
        //{
        foreach (Product item in DataSource.Productlist)//goes through the list looking for the order.
        {
            if (item.ID == productID)
                return item;
        }

        throw new Exception("Product does not exist");
        // }
        // catch(Exception ex)   { Console.WriteLine(ex); }
    }
    public void Delete(int productID)
    {
        foreach (Product item in DataSource.Productlist)//goes through the list looking for the order.
        {
            if (item.ID == productID)
            {
                DataSource.Productlist.Remove(item);
                break;
            }
        }
    }
    public void Update(Product product)
    {
        int count = 0;
        foreach (Product item in DataSource.Productlist)//goes through the list looking for the order.
        {
            if (item.ID != product.ID) count++;
            if (item.ID == product.ID)
            {
                DataSource.Productlist[count] = product;
                break;
            }
        }
    }
}
