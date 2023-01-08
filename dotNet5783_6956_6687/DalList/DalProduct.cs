using DalApi;
using DO;
using System.Reflection;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

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
        if (product.ID < 300000 || product.ID > 400000)
            throw new WrongIdException();
        if(product.Name =="" || product.Name == null)
        {
            throw new NoNameException();
        }
       if(DataSource.Productlist.Contains(product))//checks if the product already exists in the data source
       { 
            throw new Exception("Product ID already exist"); 
       }
        //foreach (Product? item in DataSource.Productlist ?? throw new NullException())
        //{
        //    if (item?.ID == product.ID)
        //        throw new Exception("Product already exist");
        //}
        DataSource.Productlist.Add(product);
        return product.ID;
    }
    /// <summary>
    /// ethod gets a product ID and delets the right orde
    /// </summary>
    /// <param name="productID"></param>
    public void Delete(int productID)
    {
        DataSource.Productlist.Remove(GetSingle(x=> x?.ID == productID));
    }
    /// <summary>
    /// method gets a product and updates its details
    /// </summary>
    /// <param name="product"></param>
    public void Update(Product product)
    {
        Delete(product.ID);
        DataSource.Productlist.Add(product);
        DataSource.Productlist= DataSource.Productlist.OrderByDescending(x=> -x?.ID ).ToList();// sorts the list by small id to bigger id
    }

    /// <summary>
    /// method returns the list 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func)
    {
        if(func == null)
        {
            return DataSource.Productlist ?? throw new NullException();//if null retun te whole list
        }
        var result = (from Product? item in DataSource.Productlist ?? throw new NullException()
                      where func(item)
                      select item);
        return result;
    }
    public Product? GetSingle(Func<Product?, bool>? func) => DataSource.Productlist.FirstOrDefault(func ?? throw new NullException()); // return a product with this id


}
