using Dal;
using DO;
using DalApi;
//using DalList;
using System.Reflection.Metadata.Ecma335;

namespace Dal;

public class program//the main where we check it works
{
    private static DalApi.IDal? dal = DalApi.Factory.Get();

    static void Main(string[] args)
    {
        Console.WriteLine(@"Please Enter:
0: To Exit
1: For Order
2: For Order Item 
3: For Product
             ");

        int choice1, choice2;
        int.TryParse(Console.ReadLine(), out choice1);
        while (choice1 >0 && choice1<4)//they want to continue
        {
               Console.WriteLine(@" 
1.Add
2.Show Using ID
3.Show List
4.Update 
5.Delete ");
                
                int.TryParse(Console.ReadLine(), out choice2);
                
                if (choice1 == 1)//order
                {
                    switch (choice2)//for order
                    {
                        case 1://to add an order to the list
                            Console.WriteLine("Enter customers name,email,address,order time, shipping date and delivery time(dd,mm,yy):");
                            string name = Console.ReadLine()!;
                            string email = Console.ReadLine()!;
                            string address = Console.ReadLine()!;
                            DateTime orderTime;
                            DateTime.TryParse(Console.ReadLine(), out orderTime);//orderTime =the date they put in
                            DateTime shipping;
                            DateTime.TryParse(Console.ReadLine(), out shipping);//shipping=the date they put in
                            DateTime delivery;
                            DateTime.TryParse(Console.ReadLine(), out delivery);//delivery=the date they put in
                            //puts all the info into the order
                            Order order = new Order();
                            order.CustomerName = name;
                            order.CustomerEmail = email;
                            order.CustomerAddress = address;
                            order.OrderDate = orderTime;
                            order.ShipDate = shipping;
                            order.DeliveryDate= delivery;
                            order.ID = dal?.order.Add(order) ?? 0 ;//adds the order to the list and returns its id
                            break;
                        case 2://Show order Using ID
                            Console.WriteLine("Enter ID");
                            int id;
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine(dal?.order.GetSingle(x=>x?.ID==id));
                            break;
                        case 3://Show List of order
                                 foreach (Order? item in (dal?.order.GetAll() ?? throw new NullException()))//goes throught he whole list of orders
                                 {
                                     Console.WriteLine(item);
                                 }
                                    break;
                        case 4://update order
                            Console.WriteLine("Enter ID");
                            int id1;
                            int.TryParse(Console.ReadLine(), out id1);
                            Console.WriteLine(dal?.order.GetSingle(x => x?.ID == id1));//prints the order before update
                            Console.WriteLine("Enter customers name,email,address:");
                            string? name1 = Console.ReadLine();
                            string? email1 = Console.ReadLine();
                            string? address1 = Console.ReadLine();
                            //puts all the info into the order
                            Order order1 = new Order();
                            order1.ID = id1;
                            order1.CustomerName = name1;
                            order1.CustomerEmail = email1;
                            order1.CustomerAddress = address1;
                            dal?.order.Update(order1);
                            break;
                        case 5://delete order
                            Console.WriteLine("Enter ID");
                            int.TryParse(Console.ReadLine(), out id1);
                            dal?.order.Delete(id1);//delets the order
                            break;
                        default:
                           throw new errorException();


                    }
                }
                if (choice1 == 2)//orderItem
                {
                    switch (choice2)
                    {
                        case 1://to add an orderItem to the list
                            OrderItem orderItem = new OrderItem();
                            Console.WriteLine("Enter Order Item Price and amount :");
                            double price;
                            double.TryParse(Console.ReadLine(), out price);
                            int amount;
                            int.TryParse(Console.ReadLine(), out amount);
                            orderItem.Price = price;
                            orderItem.Amount = amount;
                            orderItem.OrderItemID = dal?.orderItem.Add(orderItem) ?? 0 ;//adds the orderitem
                            break;

                        case 2://Show orderItem Using ID
                            Console.WriteLine("Enter ID");
                            int id;
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine(dal?.orderItem.GetSingle(x=> x?.OrderItemID== id));
                            break;
                        case 3://Show List of orderItem
                            foreach (OrderItem? item in dal?.orderItem.GetAll()?? throw new NullException())//goes through the whole list of orderItems
                            {
                                Console.WriteLine(item);
                            }
                            break;
                        case 4://update orderItem
                            Console.WriteLine("Enter Order Item ID, order ID and Product ID");
                            int id1,id2, id3;
                            int.TryParse(Console.ReadLine(), out id1);
                            int.TryParse(Console.ReadLine(), out id2);
                            int.TryParse(Console.ReadLine(), out id3);
                            Console.WriteLine(dal?.orderItem.GetSingle(x => x?.OrderItemID == id1));//prints the orderItem before update
                            Console.WriteLine("Enter Order Item Price and amount :");
                            double.TryParse(Console.ReadLine(), out price);
                            int.TryParse(Console.ReadLine(), out amount);
                            OrderItem orderItem1 = new OrderItem();
                            orderItem1.Price = price;
                            orderItem1.Amount = amount;
                            orderItem1.OrderItemID = id1;
                            orderItem1.ProductID = id3;
                            orderItem1.OrderID = id2;
                            dal?.orderItem.Update(orderItem1);
                            break;
                        case 5://delete orderItem
                            Console.WriteLine("Enter ID");
                            int.TryParse(Console.ReadLine(), out id);
                            dal?.orderItem.Delete(id);
                            break;
                        default:

                            throw new errorException();
                    }
                }
                if (choice1 == 3)// product
                {
                    switch (choice2)
                    {
                        case 1://to add a product to the list
                            Product product = new Product();
                            Console.WriteLine("Enter product name,category, price and amount");
                            product.Name = Console.ReadLine();
                            string Category1 = Console.ReadLine()!;
                            try
                            {
                                Category category = (Category)Enum.Parse(typeof(Category), Category1);
                                product.Category = category;
                            }
                            catch  { throw new doesNotExistException(); }
                       
                            double price;
                            double.TryParse(Console.ReadLine(), out price);
                            int inStock;
                            int.TryParse(Console.ReadLine(), out inStock);
                            product.Price = price;
                            product.InStock = inStock;
                            
                            product.ID = dal?.product.Add(product) ?? 0;

                            break;
                        case 2://Show product Using ID
                            Console.WriteLine("Enter ID");
                            int id;
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine(dal?.orderItem.GetSingle(x => x?.OrderItemID == id));

                            break;
                        case 3://Show List of product
                            foreach (Product? item in dal?.product.GetAll() ?? throw new NullException())//goes through the whole list and prints all the products
                            {
                                Console.WriteLine(item);
                            }

                            break;

                        case 4://update product
                            Product product1 = new Product();
                            Console.WriteLine("Enter Product ID");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine(dal?.orderItem.GetSingle(x => x?.OrderItemID == id));//prints the product before update
                            Console.WriteLine("Enter product name,category, price and amount");
                            product1.Name = Console.ReadLine();
                            string Category2 = Console.ReadLine()!;
                            Category category4 = (Category)Enum.Parse(typeof(Category), Category2);
                            double.TryParse(Console.ReadLine(), out price);
                            int.TryParse(Console.ReadLine(), out inStock);
                            product1.Price = price;
                            product1.InStock = inStock;
                            product1.Category = category4;
                            product1.ID = id;
                            dal?.product.Update(product1);//updats the product
                            break;
                        case 5://delete product
                            Console.WriteLine("Enter ID");
                            int.TryParse(Console.ReadLine(), out id);//gets an id from the user
                            dal?.product.Delete(id);//delets the product
                            break;

                        default:
                            throw new errorException();
                    }
             
                }
            
            //throw new errorException();

            Console.WriteLine(@"Please Enter:
0: To Exit
1: For Order
2: For Order Item 
3: For Product
             ");
            int.TryParse(Console.ReadLine(), out choice1);//get a new option for choice1
        }
        if (choice1 == 0)
        {
            Console.WriteLine("Good Bye");//ends the program
            return;
        }
        else
            throw new errorException();
        return;
    }
} 




//exceptions add to the file




