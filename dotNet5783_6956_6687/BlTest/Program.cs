using BlApi;
using BO;
using DalApi;
using Bl;

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
                    addPro
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default;
                }
            }
            if (choice1 == 0)
                return;
            else
                throw new errorException();
        }
    public static void OrderFunc() 
        {

        Console.WriteLine(@"Please Enter:
0: end
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
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default;
            }
        }
        if (choice1 == 0)
            return;
        else
            throw new errorException();
    }
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
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                break;
        }   
    }
    if (choice1 == 0)
        return;
    else
        throw new errorException();
}
