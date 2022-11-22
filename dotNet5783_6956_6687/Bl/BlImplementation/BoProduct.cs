using BlApi;
using BO;
using DalApi;
using DO;

namespace BlImplementation;

internal class BoProduct:IProduct
{
    private static DalApi.IDal dalList = new Dal.DalList();

    /// <summary>
    /// gets list of products(frum system) builds ProductForList and returns it
    /// </summary>
    /// <returns></returns>

    public IEnumerable<BO.ProductForList> GetProducts()
    {
        IEnumerable<BO.ProductForList> productsList = new List<BO.ProductForList>();
        return from item in dalList.product.GetAll()
               select new BO.ProductForList()
               {
                   ID = item.ID,
                   Name = item.Name,
                   Price = item.Price,
                   Category = (BO.Category)item.Category,
               };
    }

    /// <summary>
    /// gets id, if its positive gets product from system, builds a product and returnds it, throws exception if cant get the product from system
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public BO.Product ProductBuild(int Id)
    {
        if (Id >= 300000 && Id < 400000)
        {
            BO.Product product = new BO.Product();
            foreach (DO.Product item in dalList.product.GetAll())
               {
                    if (item.ID == Id)
                    {
                        product.Id = Id;
                        product.Name = item.Name;
                        product.Category = (BO.Category)item.Category;
                        product.InStock = item.InStock;
                    }
                }
                return product;
        }
        else
           throw new BO.doesNotExistException("doesnt exist"); 
    }
    /// <summary>
    /// gets id, if its positive gets product item from system, builds a product and returnds it, throws exception if cant get the product item from system
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public ProductItem ProductItemBuild(int Id)
    {
    }
    /// <summary>
    /// checks if the data is right and adds the product to the system if it can otherwise throws exeption
    /// </summary>
    /// <param name="product"></param>
    public void AddProduct(BO.Product product);
    /// <summary>
    /// gets id checks that it isnt in anybodys cart if it isnt it delets it and if it is, it throws an exception 
    /// </summary>
    /// <param name="Id"></param>
    public void DeletProduct(int Id);
    /// <summary>
    /// gets product checks if the data is right adds to the system if not throws exception
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProduct(BO.Product product);


}
