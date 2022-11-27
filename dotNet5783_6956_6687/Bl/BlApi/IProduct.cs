using BO;
namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// gets list of products(from system) builds ProductForList and returns it
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductForList> GetListOfProducts();
    /// <summary>
    /// gets id, if its positive gets product from system, builds a product and returnds it, throws exception if cant get the product from system
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Product GetProductbyID(int Id);
    /// <summary>
    /// gets id, if its positive gets product item from system, builds a product and returnds it, throws exception if cant get the product item from system
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public ProductItem ProductItemBuild(int Id);
    /// <summary>
    /// checks if the data is right and adds the product to the system if it can otherwise throws exeption
    /// </summary>
    /// <param name="product"></param>
    public void AddProduct(Product product);
    /// <summary>
    /// gets id checks that it isnt in anybodys cart if it isnt it delets it and if it is, it throws an exception 
    /// </summary>
    /// <param name="Id"></param>
    public void DeletProduct(int Id);
    /// <summary>
    /// gets product checks if the data is right adds to the system if not throws exception
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProduct(Product product);
    /// <summary>
    /// function returns a product item 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ProductItem GetProductItem(int id); 
}
