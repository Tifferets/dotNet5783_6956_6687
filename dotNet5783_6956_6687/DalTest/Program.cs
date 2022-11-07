using Dal;
using DO;
using System;
using System.Data.Common;
using DalList;
using System.Linq.Expressions;

namespace DalTest
{
    partial class program
    {
        private static DalOrder dalOrder = new DalOrder();
        private static DalProduct dalProduct = new DalProduct();
        private static DalOrderItem dalOrderItem = new DalOrderItem();
        
        static void Main(string[] args)
        {

            Console.WriteLine(@" 
0: exit
1: Order
2: Order Item 
3: Product
             ");

            int choice1;
            int.TryParse(Console.ReadLine(), out choice1);

            while (choice1 != 0)
            {
                try {
                    Console.WriteLine(@" 
1.Add
2.Show Using ID
3.Show List
4.Update 
5.Delete ");
                    int choice2;
                    int.TryParse(Console.ReadLine(), out choice2);
                    if (choice1 == 1)
                    {
                        switch (choice2)//for order
                        {
                            case 1:
                                Console.WriteLine("Enter customers name,email,address,order time, shipping date and delivery time:");
                                string name = Console.ReadLine();
                                string email = Console.ReadLine();
                                string address = Console.ReadLine();
                                DateTime delivery;
                                DateTime.TryParse(Console.ReadLine(), out delivery);

                                DateTime shipping;
                                DateTime.TryParse(Console.ReadLine(), out shipping);
                                DateTime orderTime;
                                DateTime.TryParse(Console.ReadLine(), out orderTime);
                                Order order = new Order();
                                order.CustomerName = name;
                                order.CustomerEmail = email;
                                order.CustomerAddress = address;
                                order.OrderDate = orderTime;
                                order.ShipDate = shipping;
                                order.DeliveryDate= delivery;
                                order.ID= dalOrder.Add(order);
                           

                                break;
                            case 2:
                                Console.WriteLine("Enter ID");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine(dalOrder.Get(id));
                                break;
                            case 3:
                                foreach (Order item in dalOrder.GetAll())
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case 4:
                                Console.WriteLine("Enter ID");
                                int id1;
                                int.TryParse(Console.ReadLine(), out id1);
                                Console.WriteLine("Enter customers name,email,address:");
                                string name1 = Console.ReadLine();
                                string email1 = Console.ReadLine();
                                string address1 = Console.ReadLine();
                                Order order1 = new Order();
                                order1.ID = id1;
                                order1.CustomerName = name1;
                                order1.CustomerEmail = email1;
                                order1.CustomerAddress = address1;
                                dalOrder.Update(order1);
                                break;
                            case 5:
                                Console.WriteLine("Enter ID");
                                int.TryParse(Console.ReadLine(), out id1);
                                dalOrder.Delete(id1);
                                break;
                            default:
                                throw new Exception("ERROR");


                        }
                    }
                    if (choice1 == 2)
                    {
                        switch (choice2)
                        {
                            case 1:
                                Console.WriteLine("Enter Order Item Price and amount :");
                                double price;
                                double.TryParse(Console.ReadLine(), out price);
                                int amount;
                                int.TryParse(Console.ReadLine(), out amount);
                                OrderItem orderItem = new OrderItem();
                                orderItem.Price = price;
                                orderItem.Amount = amount;
                                orderItem.OrderItemID= dalOrderItem.Add(orderItem);
                                break;

                            case 2:
                                Console.WriteLine("Enter ID");
                                int id;
                                int.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine(dalOrderItem.Get(id));
                                break;
                            case 3:
                                foreach (OrderItem item in dalOrderItem.GetAll())
                                {
                                    Console.WriteLine(item);
                                }

                                break;
                            case 4:
                                Console.WriteLine("Enter Order Item ID, order ID and Product ID");
                                int id1,id2, id3;
                                int.TryParse(Console.ReadLine(), out id1);
                                int.TryParse(Console.ReadLine(), out id2);
                                int.TryParse(Console.ReadLine(), out id3);
                                Console.WriteLine("Enter Order Item Price and amount :");
                                double.TryParse(Console.ReadLine(), out price);
                                int.TryParse(Console.ReadLine(), out amount);
                                OrderItem orderItem1 = new OrderItem();
                                orderItem1.Price = price;
                                orderItem1.Amount = amount;
                                orderItem1.OrderItemID = id1;
                                orderItem1.ProductID = id3;
                                orderItem1.OrderID = id2;
                                dalOrderItem.Update(orderItem1);
                                break;
                            case 5:
                                Console.WriteLine("Enter ID");
                                int.TryParse(Console.ReadLine(), out id);
                                dalOrderItem.Delete(id);
                                break;
                            default:

                                throw new Exception("ERROR");
                        }
                    }
                    if (choice1 == 3)// pro
                    {
                        switch (choice2)
                        {
                            case 1://add
                                Product product = new Product();
                                Console.WriteLine("Enter product name");
                                product.Name = Console.ReadLine();
                                Console.WriteLine("Enter product Category");
                                product.Category = Console.ReadLine();
                                Console.WriteLine("Enter product Price");
                                product.Price = Console.Read();
                                Console.WriteLine("Enter product amount");
                                product.InStock = Console.Read();
                               product.ID= dalProduct.Add(product);

                                break;
                            case 2:
                                Console.WriteLine("Enter ID");
                                int id = Console.Read();
                                Console.WriteLine(dalProduct.Get(id));

                                break;
                            case 3:
                                foreach (Product item in dalProduct.GetAll())
                                {
                                    Console.WriteLine(item);
                                }

                                break;

                            case 4:
                                Product product1 = new Product();
                                Console.WriteLine("Enter product name");
                                product1.Name = Console.ReadLine();
                                Console.WriteLine("Enter product Category");
                                product1.Category = Console.ReadLine();
                                Console.WriteLine("Enter product Price");
                                product1.Price = Console.Read();
                                Console.WriteLine("Enter product amount");
                                product1.InStock = Console.Read();
                                dalProduct.Update(product1);
                                break;
                            case 5:
                                Console.WriteLine("Enter ID");
                                int id1 = Console.Read();
                                dalProduct.Delete(id1);
                                break;

                            default:
                                throw new Exception("ERROR");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                Console.WriteLine(@" 
0: exit
1: Order
2: Order Item 
3: Product
             ");
                int.TryParse(Console.ReadLine(), out choice1);
            }

        }
    } 
    

}




