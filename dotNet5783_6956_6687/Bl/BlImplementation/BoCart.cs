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
    public Cart AddCart(Cart cart, int productId)
    {
        if (productId < 300000 || productId > 499999)
            throw new WrongIDException();
        BO.OrderItem? p = new BO.OrderItem() ;
        foreach (DO.Product item in dalList.product.GetAll())
        {
            if (item.ID == productId)
            { 
                p.ProductID=item.ID;
                p.Price = item.Price;
                p.Name = item.Name;
                break;
            }
        } 
        cart.Items.ProductID = productId;
        cart.Items.Amount = 0;
        cart.Items.Price = p.Price;
        cart.Items.
        return cart;
    }
    /// <summary>
    /// update an amount of a product in the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <param name="newAmount"></param>
    public Cart UpdateCart(Cart cart, int productId, int newAmount)
    {
        foreach(BoCart.dalList.orderItem item in BoCart.dalList.)
        {
            if(item)
        }
    }
    /// <summary>
    /// the function confirms the cart get, gets the cart and the customers info
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="name"></param>
    /// <param name="address"></param>
    /// <param name="email"></param>
    public void confirmCart(Cart cart, string name, string address, string email);
}
