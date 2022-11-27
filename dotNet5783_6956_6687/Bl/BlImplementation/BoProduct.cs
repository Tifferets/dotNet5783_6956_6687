using BlApi;
using System.Linq.Expressions;
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
        try
        {
            return from item in dalList.product.GetAll()
                   select new BO.ProductForList()
                   {
                       ID = item.ID,
                       Name = item.Name,
                       Price = item.Price,
                       Category = (BO.Category)item.Category,
                   };
        }
        catch
        {
            throw new BO.errorException();
        }
        
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
            try
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
            catch
            {
                throw new BO.CantGetException();
            }
        }
        else
            throw new BO.doesNotExistException(); 
    }
    /// <summary>
    /// gets id, if its positive gets product item from system, builds a product and returnds it, throws exception if cant get the product item from system
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public BO.ProductItem ProductItemBuild(int Id)//has different stuff in both papers
    {
        if (Id >= 300000 && Id < 400000)
        {
            BO.ProductItem productItem = new BO.ProductItem();
            foreach (DO.Product item in dalList.product.GetAll())
            {
                if (item.ID == Id)
                {
                    productItem.ID = Id;
                    productItem.Name = item.Name;
                    productItem.Category = (BO.Category)item.Category;
                    productItem.Amount = item.InStock;
                }
            }
            return productItem;
        }
        else
        {
            throw new BO.doesNotExistException(); 
        }

    }
    /// <summary>
    /// function adds a product, checks if the data is right and adds the product to the system if it can otherwise throws exeption
    /// </summary>
    /// <param name="product"></param>
    public void AddProduct(BO.Product product)
    {
        if (checkDataIsGood(product))
        {
            foreach (DO.Product item in dalList.product.GetAll())
            {
                if (item.ID == product.Id)
                {
                    throw new BO.alreadyExistException();
                }
            }
            try
            {
                dalList.product.Add(convert(product));
            }
            catch
            {
                throw new BO.WrongDataException();
            }
        }
        else
            throw new BO.WrongDataException();
    }
    /// <summary>
    /// function that gets a product and checks if its data is all correct returns true
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public bool checkDataIsGood(BO.Product product)
    {
        if(product.Id >=300000 && product.Id < 400000)
        {
            if(product.Name != null && product.Price>0 && product.InStock >= 0)
                return true;
        }
        return false;
    }
    /// <summary>
    /// function to convert from BO to DO
    /// </summary>
    /// <param name="p1"></param>
    /// <returns></returns>
    public DO.Product convert(BO.Product p1)
    {
        DO.Product product=new DO.Product();
        product.ID=p1.Id;
        product.Name=p1.Name;
        product.Price=p1.Price;
        product.InStock=p1.InStock;
        product.Category= (DO.Category)p1.Category;
        return product;
    }
    /// <summary> 
    /// delete a product, gets id checks that it isnt in anybodys cart if it isnt it delets it and if it is, it throws an exception 
    /// </summary>
    /// <param name="Id"></param>
    public void DeletProduct(int Id)
    {
        IEnumerable<DO.Order> orderList = dalList.order.GetAll();
        foreach (DO.Order item in orderList )
        {
            IEnumerable<DO.OrderItem> orderItemList = dalList.order.GetAllOrderItems();
            foreach(DO.OrderItem oitem in orderItemList)
            {
                if(item.ID == Id)
                {
                    throw new BO.CantDeleteException();
                }
            }

        }
        try
        {
            dalList.product.Delete(Id);
        }
        catch
        {
            throw new BO.CantDeleteException();
        }
    }
    /// <summary>
    /// updates product, gets product checks if the data is right adds to the system if not throws exception
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProduct(BO.Product product)
    {
        //IEnumerable<DO.Product> productList = dalList.product.GetAll();
        if (checkDataIsGood(product))
        {
            try
            {
                dalList.product.Update(convert(product));//trys to update te product
            }
            catch
            {
                throw new BO.CantUpDateException();
            }
        }
        else
        {
            throw new BO.CantUpDateException();
        }
        
    }
}
