using Dal;
using DO;
using System;
using System.Data.Common;
using DalList;

namespace DalTest
{
    partial class program
    {
        private DalOrder dalOrder = new DalOrder();
        private DalProduct dalProduct = new DalProduct();
        private DalOrderItem dalOrderItem = new DalOrderItem();
        static void Main(string[] args)
        {

        Console.WriteLine(@" 
0: exit
1: Order
2: Order Item 
3:Product
             ");

        int choice1 = Console.Read();
        while (choice1 !=0)
    {
        Console.WriteLine(@" 
            1.Add
            2.Show Using ID
            3.Show List
            4.Update 
            5.Delete ");
            int choice2 = Console.Read();
            if(choice1== 1)
                 {
                    switch (choice1)//for order
                     {
                    case 1:
                            Console.WriteLine("Enter customers name,email,address:");
                            string name = Console.ReadLine();
                            string email = Console.ReadLine();
                            string address = Console.ReadLine();
                            Order order = new Order();
                            order.CustomerName = name;
                            order.CustomerEmail = email;
                            order.CustomerAddress=address;  
                           // Da

                            break;
                    case 2:
                        break;
                    case 3:
                            foreach(Order item in )
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                     } 
                  }
                if (choice1 == 2)
                { 
                    switch (choice1)
                    {
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
                        product.Category= Console.ReadLine();
                        Console.WriteLine("Enter product Price");
                        product.Price = Console.Read();
                        Console.WriteLine("Enter product amount");
                        product.InStock = Console.Read();
                        
                        break;
                    case 2:
                        Console.WriteLine("Enter ID");
                        int id = Console.Read();
                        

                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                }
            }
    }
}
}




