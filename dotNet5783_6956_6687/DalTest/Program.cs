using Dal;

using System;
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
2: Product
3: Order Item
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
                if (choice1 == 3)
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
        }
    }
}





