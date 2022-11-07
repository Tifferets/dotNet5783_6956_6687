using DO;

namespace Dal;

public class DalProduct
{
    public int Add(Product product)
    {
        foreach(Product item in DataSource.Productlist)
        {
            if(item.ID == product.ID)
                throw new Exception("Product already exist");
        }
        DataSource.Productlist.Add(product);
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
    public List<Product> GetAll()=> DataSource.Productlist; 
    
}
