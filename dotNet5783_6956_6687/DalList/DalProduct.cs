using DalApi;
using DO;
using System.Reflection;

namespace Dal;

internal class DalProduct : IProduct
{
    /// <summary>
    /// method that gets a product, adds to the list
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product product)
    {
        foreach (Product item in DataSource.Productlist)
        {
            if (item.ID == product.ID)
                throw new Exception("Product already exist");
        }
        DataSource.Productlist.Add(product);
        return product.ID;
    }
    /// <summary>
    /// method gets a product ID and renurns the product it belongs to
    /// </summary>
    /// <param name="productID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    //public Product Get(int? productID)
    //{
    //    //try
    //    //{
    //    foreach (Product item in DataSource.Productlist)//goes through the list looking for the order.
    //    {
    //        if (item.ID == productID)
    //            return item;
    //    }

    //    throw new Exception("Product does not exist");
    //    // }
    //    // catch(Exception ex)   { Console.WriteLine(ex); }
    //}
    /// <summary>
    /// ethod gets a product ID and delets the right orde
    /// </summary>
    /// <param name="productID"></param>
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
    /// <summary>
    /// method gets a product and updates its details
    /// </summary>
    /// <param name="product"></param>
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

    static bool isProduct(Product p)
    {
        return true;
    }

    /// <summary>
    /// method returns the list 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func)
    {
        if(func == null)
        {
            return DataSource.Productlist;//if null retun te whole list
        }
        List<Product?> result = new List<Product?>();
        foreach(var item in DataSource.Productlist)
        {
            if(func(item))//if the id is good
                result.Add(item);//adds to list 
        }
        return result;
    }
    public Product? GetSingle(Func<Product?, bool>? func) => DataSource.Productlist.First(func); // return a product with this id


}
