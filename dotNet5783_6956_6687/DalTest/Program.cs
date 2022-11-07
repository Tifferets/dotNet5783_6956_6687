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

            //  int choice1 = Console.Read();
            //choice1=  int.Parse(choice1);
            int choice1 = 1;
            while (choice1 != 0)
            {
                try {
                    Console.WriteLine(@" 
            1.Add
            2.Show Using ID
            3.Show List
            4.Update 
            5.Delete ");
                    int choice2 = Console.Read();
                    Console.WriteLine(choice2);
                    if (choice1 == 1)
                    {
                        switch (choice2)//for order
                        {
                            case 1:
                                Console.WriteLine("Enter customers name,email,address:");
                                string name = Console.ReadLine();
                                string email = Console.ReadLine();
                                string address = Console.ReadLine();
                                Order order = new Order();
                                order.CustomerName = name;
                                order.CustomerEmail = email;
                                order.CustomerAddress = address;
                                dalOrder.Add(order);

                                break;
                            case 2:
                                Console.WriteLine("Enter ID");
                                int id = Console.Read();
                                dalOrder.Get(id);
                                break;
                            case 3:

                                //foreach (Order item in  )
                                //{

                                //}
                                break;
                            case 4:
                                Console.WriteLine("Enter customers name,email,address:");
                                string name1 = Console.ReadLine();
                                string email1 = Console.ReadLine();
                                string address1 = Console.ReadLine();
                                Order order1 = new Order();
                                order1.CustomerName = name1;
                                order1.CustomerEmail = email1;
                                order1.CustomerAddress = address1;
                                dalOrder.Update(order1);
                                break;
                            case 5:
                                Console.WriteLine("Enter ID");
                                int id1 = Console.Read();
                                dalOrder.Delete(id1);
                                break;
                            default:
                                throw new Exception("ERROR");


                        }
                    }
                    if (choice1 == 2)
                    {
                        switch (choice1)
                        {
                            case 1:
                                Console.WriteLine("Enter Order Item Price and amount :");
                                double price = Console.Read();
                                int amount = Console.Read();
                                OrderItem orderItem = new OrderItem();
                                orderItem.Price = price;
                                orderItem.Amount = amount;
                                dalOrderItem.Add(orderItem);
                                break;

                            case 2:
                                Console.WriteLine("Enter ID");
                                int id = Console.Read();
                                dalOrderItem.Get(id);
                                break;
                            case 3:


                                break;
                            case 4:
                                Console.WriteLine("Enter Order Item Price and amount :");
                                double price1 = Console.Read();
                                int amount1 = Console.Read();
                                OrderItem orderItem1 = new OrderItem();
                                orderItem1.Price = price1;
                                orderItem1.Amount = amount1;
                                break;
                            case 5:
                                Console.WriteLine("Enter ID");
                                int id1 = Console.Read();
                                dalOrderItem.Delete(id1);
                                break;
                            default:

                                throw new Exception("ERROR");
                        }
                    }
                    if (choice1 == 3)// pro
                    {
                        switch (choice1)
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
                                dalProduct.Add(product);

                                break;
                            case 2:
                                Console.WriteLine("Enter ID");
                                int id = Console.Read();
                                dalProduct.Get(id);

                                break;
                            case 3:

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


                choice1 = Console.Read();
            }

        }
    } }




