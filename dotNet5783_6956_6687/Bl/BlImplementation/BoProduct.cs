using BlApi;
using BO;

namespace BlImplementation;

internal class BoProduct : IProduct
{
    private static DalApi.IDal dalList = new Dal.DalList();

    /// <summary>
    /// gets list of products(from system) builds ProductForList and returns it
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList?> GetproductForListByCategory(BO.Category category)
    {
        List<BO.ProductForList> products = new List<BO.ProductForList>();//new list
        try
        {
            foreach (DO.Product item in dalList.product.GetAll())//goes over all products
            {
                if ((BO.Category)item.Category == category)//checks if the category is the same
                {
                    bool flag = false;//adds the product
                    if (item.InStock > 0)
                    {
                        flag = true;
                    }
                    BO.ProductForList productForList = new BO.ProductForList() { ID = item.ID, Name = item.Name, InStock = flag, Amount = item.InStock, Price = item.Price, Category = (BO.Category)item.Category };
                    products.Add(productForList);
                }
            }
            return products;
        }
        catch (Exception ex)
        {
            throw new BO.errorException();
        }
    }

    public IEnumerable<BO.ProductForList> GetListOfProducts()
    {
        List<BO.ProductForList?> products = new List<BO.ProductForList>();
        try
        {
            foreach (DO.Product item in dalList.product.GetAll())
            {
                bool flag = false;
                if (item.InStock > 0)
                {
                    flag = true;
                }
                BO.ProductForList productForList = new BO.ProductForList() { ID = item.ID, Name = item.Name, InStock = flag, Amount = item.InStock, Price = item.Price, Category = (BO.Category)item.Category };
                products.Add(productForList);
            }
            return products;
        }
        catch (Exception ex)
        {
            throw new BO.errorException();
        }


    }

    /// <summary>
    /// gets id, if its positive gets product from system, builds a product and returnds it, throws exception if cant get the product from system
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public BO.Product GetProductbyID(int Id)
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
                        product.Price = item.Price;
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
    private bool checkDataIsGood(BO.Product product)
    {
        if (product.Id >= 300000 && product.Id < 400000)
        {
            if (product.Name != null && product.Price > 0 && product.InStock >= 0)
                return true;
        }
        return false;
    }
    /// <summary>
    /// function to convert from BO to DO
    /// </summary>
    /// <param name="p1"></param>
    /// <returns></returns>
    private DO.Product convert(BO.Product p1)
    {
        DO.Product product = new DO.Product();
        product.ID = p1.Id;
        product.Name = p1.Name;
        product.Price = p1.Price;
        product.InStock = p1.InStock;
        product.Category = (DO.Category)p1.Category;
        return product;
    }
    /// <summary> 
    /// delete a product, gets id checks that it isnt in anybodys cart if it isnt it delets it and if it is, it throws an exception 
    /// </summary>
    /// <param name="Id"></param>
    public void DeletProduct(int Id)
    {
        if (Id >= 300000 && Id < 400000)
        {
            IEnumerable<DO.Order?> orderList = dalList.order.GetAll();//list of do.order
            foreach (DO.Order item in orderList)//going over the list
            {
                IEnumerable<DO.OrderItem?> orderItemList = dalList.order.GetAllOrderItems(item.ID);//gets a list of all order items for the order
                foreach (DO.OrderItem oitem in orderItemList)//
                {
                    if (oitem.ProductID == Id)//checks if the product is in the orderitem
                    {
                        throw new BO.CantDeleteException();
                    }
                }
            }
            try
            {
                dalList.product.Delete(Id);
            }
            catch (Exception ex)
            {
                throw new BO.CantDeleteException();
            }
        }
        else
        {
            throw new BO.WrongIDException();
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
    /// <summary>
    /// function returns a product item 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.ProductItem GetProductItem(int id)//didnt get CART CAUSE DIDNT SEE USE FOR IT
    {
        if (id < 300000)
        {
            throw new BO.errorException();
        }
        try
        {
            DO.Product product = (DO.Product)dalList.product.GetSingle(x => x?.ID == id);//gets the first in the list 
            bool flag = false;
            if (product.InStock > 0)
            {
                flag = true;
            }
            BO.ProductItem productItem = new BO.ProductItem()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Category = (BO.Category)product.Category,
                Amount = product.InStock,
                Instock = flag
            };
            return productItem;
        }
        catch
        {
            throw new BO.errorException();
        }
    }
}