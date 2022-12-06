using BlApi;
using Dal;
using BO;
using DalApi;
using Bl;
using System.Numerics;
namespace BL;

internal class Program
{
    private static BlApi.IBl blList = new BlImplementation.Bl();
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
        while (choice > -1 && choice < 4)
        {
            try
            {
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
                default: throw new BO.errorException();
            }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            Console.WriteLine(@"Please Enter:
0: To Exit
1: For Cart
2: For Order
3: For Product
");
            int.TryParse(Console.ReadLine(), out choice);
        }    
    }
    /// <summary>
    /// fuction that uses all the cart functions 
    /// </summary>
    /// <exception cref="BO.errorException"></exception>
    public static void CartFunc()
    {

        List<BO.OrderItem> orderItems = new List<BO.OrderItem>() { new OrderItem { ID = 200000, Name = "Big dog", Price = 63, ProductID = 300007, Amount = 4, TotalPrice = 63*4, } };
        Cart cart = new Cart()//card to check the main with
        {
            Items = orderItems,
            CustomerAddress= "beit shemesh",
            CustomerEmail = "customer@gmail.com",
            CustomerName= "customer",
            TotalPrice= 63*4,
        };
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
                    Console.WriteLine("enter product ID");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);  
                    cart= blList.Cart.AddProductToCart(cart, id);//adds a product to the cart

                    break;
                case 2:
                    Console.WriteLine("enter custemer name, product id and new amount ");
                    int productid, newAmount;
                    string name = Console.ReadLine();//we dont relly need this its a way to identidy the cart but we only have one cart so we wont us it 
                    int.TryParse(Console.ReadLine(), out productid);
                    int.TryParse(Console.ReadLine(), out newAmount);
                    cart = blList.Cart.UpdateAmountOfProductInCart(cart, productid, newAmount);//updates an amount of the product in the cart
                    break;
                case 3:
                    Console.WriteLine("enter customers name,address and email");
                    name = Console.ReadLine();
                    string address = Console.ReadLine();
                    string email = Console.ReadLine();
                    blList.Cart.confirmCart(cart, name, address, email);
                    
                    break;
                default:
                    throw new BO.errorException();
            }
            Console.WriteLine(@"Please Enter:
0: To go back to main menu
1: To add a product to the cart
2: To update an amount of a product
3: To confirm cart
");
            int.TryParse(Console.ReadLine(), out choice1);
        }
        if (choice1 == 0)
            return;
        else
            throw new BO.errorException();

    }
    /// <summary>
    /// function to use all the order functions
    /// </summary>
    /// <exception cref="BO.errorException"></exception>
    public static void OrderFunc() 
    {

        Console.WriteLine(@"Please Enter:
0: To go back to main menu
1: To get a list of all the orders
2: To see an orders info
3: To update shipping date
4: To update delivery date
5: To see an orders status
6: To update order
");
        int choice1;
        int.TryParse(Console.ReadLine(), out choice1);
        List<BO.OrderItem> orderItems = new List<BO.OrderItem>() { new OrderItem { ID = 200000, Name = "Big dog", Price = 63, ProductID = 300007, Amount = 4, TotalPrice = 63 * 4, } };
        Order order1 = new Order()
        {
            ID= 100000, CustomerAddress= "tel aviv", CustomerEmail= "yael@gmail.com", CustomerName="yael",
            DeliveryDate= null, OrderDate= null, ShipDate = null, 
            Items= orderItems,
        };
        while (choice1 > 0 && choice1 < 6)
        {
            switch (choice1)
            {
                case 1:
                    foreach (BO.OrderForList item in blList.Order.GetOrderList())//gets all orders in order list- no date
                    {
                        Console.WriteLine(item);//print all
                    }
                    break;

                case 2:
                    Console.WriteLine("enter orders id");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    Order order =blList.Order.GetOrderInfo(id);//gets order info for the admin
                    Console.WriteLine(order);
                    break;
                case 3:
                    Console.WriteLine("enter orders id");
                    int id1;
                    int.TryParse(Console.ReadLine(), out id1);
                    order1 = blList.Order.UpdateShippingDate(order1.ID);// updates order shipping date
                    break;
                case 4:
                    Console.WriteLine("enter orders id");
                    int.TryParse(Console.ReadLine(), out id1);
                    order1 = blList.Order.UpdateDeliveryDate(order1.ID);//updates order dilivery date
                    break;
                case 5://To see an orders status
                    Console.WriteLine("enter orders id");
                    int.TryParse(Console.ReadLine(), out id1);
                    Console.WriteLine(blList.Order.OrderStatus(id1));//prints order traking details
                    break;
                default:
                    throw new BO.errorException();
            }
           
            Console.WriteLine(@"Please Enter:
0: To back
1: To get a list of all the orders
2: To see an orders info
3: To update shipping date
4: To update delivery date
5: To see an orders status
6: To update order
");
            int.TryParse(Console.ReadLine(), out choice1);
        }
        if (choice1 == 0)
            return;
    }

    /// <summary>
    /// function to use all the product functions
    /// </summary>
    /// <exception cref="BO.doesNotExistException"></exception>
    /// <exception cref="BO.errorException"></exception>
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
    while (choice1 >= 0 && choice1 < 7)
    {
        switch (choice1)
        {
            case 0: 
                return;//goes back to the 3 options 
            case 1://To get list of all the products
                     foreach(BO.ProductForList item in blList.Product.GetListOfProducts())
                        {
                            Console.WriteLine(item);//print all
                        }
                    break;

            case 2://To get a products details- for admin
                
                      Console.WriteLine("Enter product Id");
                      int id;
                      int.TryParse(Console.ReadLine(), out id);
                      BO.Product product = blList.Product.GetProductbyID(id);//getting product info
                      Console.WriteLine(product);//printing product
                      break;
                
            case 3://To get a products details- for customer
                    Console.WriteLine("Enter product Id");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine(blList.Product.GetProductItem(id)); //retuns and prints the details
                    break;
            case 4:// To add a product
                    Console.WriteLine("Please enter product Id, name,category, amount in stock and price");
                    int.TryParse(Console.ReadLine(), out id);
                    BO.Product product1 = new BO.Product();
                    product1.Id=id;
                    product1.Name = Console.ReadLine();
                    string Category = Console.ReadLine();
                    try
                    {
                        Category category = (Category)BO.Enum.Parse(typeof(Category), Category);//converting to enum type
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
                    blList.Product.AddProduct(product1);//adding the product 
                    break;

            case 5://To delete a product
                    Console.WriteLine("Enter product Id");
                    int.TryParse(Console.ReadLine(), out id);
                    blList.Product.DeletProduct(id);//deleting the product
                    break;
            case 6:
                    Console.WriteLine("Please enter product Id, name,category, amount in stock and price");
                    int.TryParse(Console.ReadLine(), out id);
                    product1 = new BO.Product();
                    product1.Id = id;
                    product1.Name = Console.ReadLine();
                    Category = Console.ReadLine();
                    try
                    {
                        Category category = (Category)BO.Enum.Parse(typeof(Category), Category);//converting to enum type
                        product1.Category = category;
                    }
                    catch
                    {
                        throw new BO.doesNotExistException();
                    }
                    double.TryParse(Console.ReadLine(), out price);
                    int.TryParse(Console.ReadLine(), out inStock);
                    product1.Price = price;
                    product1.InStock = inStock;
                    blList.Product.UpdateProduct(product1);//updating the product
                    break;
            default:
                    throw new BO.errorException();
                break;
        }
            Console.WriteLine(@"Please Enter:
0: To end program
1: To get list of all the products
2: To get a products details- for admin
3: To get a products details- for customer
4: To add a product
5: To delete a product
6: To update a product

");
            int.TryParse(Console.ReadLine(), out choice1);
    }
    if (choice1 == 0)
        return;
    }
}
/*
 Please Enter:
0: To Exit
1: For Cart
2: For Order
3: For Product

1
Please Enter:
0: back
1: add a product to the cart
2: update an amount of a product
3: confirm cart

3
enter customers name,adrees and email
aaa
bbb
ccc@
cart confirmed
Please Enter:
0: To go back to main menu
1: To add a product to the cart
2: To update an amount of a product
3: To confirm cart

0
Please Enter:
0: To Exit
1: For Cart
2: For Order
3: For Product

0
Good Bye and thanks for shopping
 */