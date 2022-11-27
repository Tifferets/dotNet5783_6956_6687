using BlApi;
using Dal;
using BO;
using DalApi;
using Bl;
//using BoCart;

namespace BL;

internal class Program
{
    private static BlApi.IBl blList = new BlImplementation.Bl();
    internal static List<Cart> carts = new List<Cart>();
   // internal static List<OrderItem> OrderItemList = new List<OrderItem>();
   // internal static List<Product> Productlist = new List<Product>();
    static void Main(string[] args)
    {
        
    Console.WriteLine(@"Please Enter:
0: To Exit
1: For Cart
2: For Order
3: For Product
");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            while (choice > 0 && choice < 4) {
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Good Bye and thanks for shopping");//ends the program
                        return;
                    case 1:
                        CartFunc();
                        break;
                    case 2:
                        OrderFunc();
                        break;
                    case 3:
                        ProductFunc();
                        break;
                    default
                }
            }
            if (choice == 0)
            { 
                
            }

    }
   
    public static void CartFunc()
    {
        Console.WriteLine(@"Please Enter:
0: back
1: add a product to the cart
2: update an amount of a product
3: confirm cart
");
            int choice1;
            int.TryParse(Console.ReadLine(), out choice1);
            while (choice1 > 0 && choice1 < 4)
            {
                switch (choice1)
                {
                    case 1:
                    //  BO.Cart cart = new BO.Cart();
                    Console.WriteLine("enter product ID");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    Cart cart = new Cart();
                    blList.Cart.AddProductToCart(cart, id);

                        break;
                    case 2:
                    Console.WriteLine("enter cart id, product id and new amount ");
                    int cartid,productid,newAmount;
                    int.TryParse(Console.ReadLine(), out cartid);
                    int.TryParse(Console.ReadLine(), out productid);
                    int.TryParse(Console.ReadLine(), out newAmount);
                    foreach(OrderItem item in cart.Items)
                    {
                        if(item.Items)
                    }
                        break;
                    case 3:
                        break;
                    default;
                }
            }
            if (choice1 == 0)
                return;
            else
                throw new BO.errorException();
        }
    public static void OrderFunc() 
        {

        Console.WriteLine(@"Please Enter:
0: back
1: get a list of all the orders
2: see an orders info
3: update shipping date
4: update delivery date
5: see one orders status
6: update order
");
        int choice1;
        int.TryParse(Console.ReadLine(), out choice1);
        while (choice1 > 0 && choice1 < 4)
        {
            switch (choice1)
            {
                case 1:
                    IEnumerable<Order> lst = blList.Order.GetOrderList();
                    foreach(Order item in lst)
                        Console.WriteLine(item);
                    break;
                case 2:
                    Console.WriteLine("enter orders id");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    Order order =blList.Order.GetOrderInfo(id);
                    Console.WriteLine(order);
                    break;
                case 3:
                    Console.WriteLine("enter orders id");
                    int id1;
                    int.TryParse(Console.ReadLine(), out id1);
                    Order order1 = blList.Order.UpdateShippingDate(id1);
                    break;
                case 4:
                    Console.WriteLine("enter orders id");
                    int.TryParse(Console.ReadLine(), out id1);
                    Order order2 = blList.Order.UpdateDeliveryDate(id1);
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default;

            }
        }
        if (choice1 == 0)
            return;
        else
            throw new BO.errorException();
    }


    public static void ProductFunc() 
{
    Console.WriteLine(@"Please Enter:
0: To end program
1: To get list of all the products
2: To get a products details- for admin
3: To get a products details- for customer
4: To add a product
5: To delete a product
6: To update a product

");
    int choice1;
    int.TryParse(Console.ReadLine(), out choice1);
    while (choice1 > 0 && choice1 < 4)
    {
        switch (choice1)
        {
            case 0: 
                return;//goes back to the 3 options 
            case 1://To get list of all the products
                {
                      foreach(BO.ProductForList item in blList.Product.GetListOfProducts())
                        {
                            Console.WriteLine(item);//print all
                        }
                    break;
                }
            case 2://To get a products details- for admin
                {
                      Console.WriteLine("Enter product Id");
                      int id;
                      int.TryParse(Console.ReadLine(), out id);
                      BO.Product product = blList.Product.GetProductbyID(id);//getting product info
                      Console.WriteLine(product);//printing product
                      break;
                }
            case 3://To get a products details- for customer
                    Console.WriteLine("Enter product Id");
                    int id2;
                    int.TryParse(Console.ReadLine(), out id2);
                    Console.WriteLine(blList.Product.GetProductItem(id2)); //retuns and prints the details
                    break;
            case 4:// To add a product
                    Console.WriteLine("Please enter product Id, name, amount in stock and category ");
                    BO.Product product1 = new BO.Product();
                    product1.Name = Console.ReadLine();
                    string Category = Console.ReadLine();
                    try
                    {
                        Category category = (Category)BO.Enum.Parse(typeof(Category), Category);
                        product1.Category = category;
                    }
                    catch
                    { 

                        throw new BO.doesNotExistException(); 
                    }
                    double price;
                    double.TryParse(Console.ReadLine(), out price);
                    int inStock;
                    int.TryParse(Console.ReadLine(), out inStock);
                    product1.Price = price;
                    product1.InStock = inStock;
                    blList.Product.AddProduct(product1);
                    break;
            case 5://To delete a product
                    Console.WriteLine("Enter product Id");
                    int id1;
                    int.TryParse(Console.ReadLine(), out id1);
                    blList.Product.DeletProduct(id1);
                    break;
            case 6:
                    Console.WriteLine("Please enter product Id, name, amount in stock and category ");
                    BO.Product product2 = new BO.Product();
                    product2.Name = Console.ReadLine();
                    string Category1 = Console.ReadLine();
                    try
                    {
                        Category category = (Category)BO.Enum.Parse(typeof(Category), Category1);
                        product2.Category = category;
                    }
                    catch
                    {

                        throw new BO.doesNotExistException();
                    }
                    double price1;
                    double.TryParse(Console.ReadLine(), out price);
                    int inStock1;
                    int.TryParse(Console.ReadLine(), out inStock);
                    product2.Price = price;
                    product2.InStock = inStock;
                    blList.Product.UpdateProduct(product2);
                    break;
            default:
                    throw new BO.errorException();
                break;
        }   
    }
    if (choice1 == 0)
        return;
    else
        throw new errorException();
}
