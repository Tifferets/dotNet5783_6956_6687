using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class BoCart : ICart
{
    private static DalApi.IDal dalList = new Dal.DalList();
    /// <summary>
    /// adds a product to the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Cart AddProductToCart(Cart cart, int productId)
    {
        bool flag=false;
        if (productId < 300000 || productId > 499999)
            throw new WrongIDException();
        //foreach (DO.Product item in dalList.product.GetAll())
        // BO.OrderItem? p = new BO.OrderItem();
        // IEnumerable<BO.OrderItem> carts = cart.Items;

        foreach (BO.OrderItem item in cart.Items)//goes through all the items in the cart
        {
            if (item.ProductID == productId)//if the product already exists in the cart
            {
                flag = true;

                foreach (DO.Product item1 in dalList.product.GetAll())//goes through all the products that exist in general
                {
                    if (item1.ID == productId && item1.InStock>0)//if it exists and has aenough in stock
                    {
                        item.Amount = item.Amount + 1;//addes 1 more of the product to the cart
                        item.TotalPrice = item.TotalPrice + item.Price;
                        cart.TotalPrice = item.TotalPrice;
                        break;
                    }
                    else
                        throw new NoMoreInStockException();
                }
                break;
            }
        }
        if (flag == false)//if the product does not exists in the cart
        {
            foreach (DO.Product item in dalList.product.GetAll())//goes through all the products that exist in general
            {
                if (item.ID == productId && item.InStock > 0)//if it exists and has aenough in stock
                {
                    BO.OrderItem newItem = new BO.OrderItem()
                    {
                        ProductID = productId,
                        ID = item.ID,
                        Name = item.Name,
                        Price = item.Price,
                        TotalPrice = item.Price,
                        Amount = 1
                    };
                    cart.TotalPrice = newItem.TotalPrice + cart.TotalPrice;
                }
                else 
                    throw new NoMoreInStockException();
            }
        }
        return cart;
    }

    /// <summary>
    /// update an amount of a product in the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <param name="newAmount"></param>
    public Cart UpdateAmountOfProductInCart(Cart cart, int productId, int newAmount)
    {
        int? dif = 0;//the difference between the new amount and the old amount
        List <BO.OrderItem> lst = new List<BO.OrderItem>();
        foreach(OrderItem item in cart.Items)
        {
            lst.Add(item);//copies all the list in cart to the lst
           // if (item.ID == productId)//if the product already exists then amount= the amount of the product in the cart
               // amount = item.Amount;
        }
        foreach(BO.OrderItem item in cart.Items)
        {
            if(item.ProductID==productId)
            {
                if (newAmount == 0) 
                {
                    lst.Remove(item);//removes the orderitem from lst
                    cart.Items = lst;//updates the cart
                    break;
                }
                if (newAmount >item.Amount)//wanted more
                {
                    dif= newAmount-item.Amount;
                    cart.TotalPrice = cart.TotalPrice + dif * item.Price;
                    item.Amount = newAmount;
                    break;
                }
                if (newAmount < item.Amount)//wanted less
                {
                    dif = newAmount - item.Amount;
                    cart.TotalPrice = cart.TotalPrice - dif * item.Price;
                    item.Amount = newAmount;
                    break;
                }
            }
        }
        return cart;
    }
    /// <summary>
    /// the function confirms the cart get, gets the cart and the customers info
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="name"></param>
    ///<param name="address"></param>
    /// <param name="email"></param>
    public void confirmCart(Cart cart, string name, string address, string email)
    {
        if (name == null || address == null || email == null)//need to check validation for email
            throw new MissingCustomersInfoException();
        foreach(OrderItem item in cart.Items)
        {
            if (item.Amount < 0)
                throw new WrongAmountException();
            bool flag = false;
            foreach (DO.Product item1 in dalList.product.GetAll())//goes through all the product looking for the product in the cart
            {
                if (item1.ID == item.ID)
                {
                    if (item1.InStock <= 0)//if the product exists but its not in stock
                        throw new NoMoreInStockException();
                    else
                        flag = true;
                    break;//move on to look for the next product
                }
            }
            if (flag == false)//if we didnt find the product in general then throw
                throw new NoMoreInStockException();
        }
    }
}
