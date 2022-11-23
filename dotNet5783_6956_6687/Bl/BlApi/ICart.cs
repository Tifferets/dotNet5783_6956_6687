using BO;
namespace BlApi;

public interface ICart
{
    /// <summary>
    /// adds a product to the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Cart AddProductToCart(Cart cart, int productId);
    /// <summary>
    /// update an amount of a product in the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <param name="newAmount"></param>
    public Cart UpdateAmountOfProductInCart(Cart cart, int productId, int newAmount);
    /// <summary>
    /// the function confirms the cart get, gets the cart and the customers info
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="name"></param>
    /// <param name="address"></param>
    /// <param name="email"></param>
    public void confirmCart(Cart cart, string name, string address, string email);
}
