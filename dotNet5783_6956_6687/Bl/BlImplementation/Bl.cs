using BlApi;

namespace BlImplementation;

internal class Bl:IBl
{
    public ICart Cart{ get; } = new BlImplementation.BoCart();
    public IOrder Order { get; } = new BlImplementation.BoOrder();
    public IProduct Product { get; } = new BlImplementation.BoProduct();

}
