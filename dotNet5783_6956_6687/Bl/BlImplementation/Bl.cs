using BlApi;

namespace BlImplementation;

internal class Bl:IBl
{
    public ICart Cart=> new BoCart();
    public IOrder Order => new BoOrder();
    public IProduct Product => new BoProduct();

}
