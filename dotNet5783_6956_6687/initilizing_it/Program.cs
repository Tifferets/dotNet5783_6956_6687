using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalXml;
namespace Dal;
public class hi
{
    public class program
    {
        #region initializes details
        internal static readonly Random rand = new Random();//to generat random 
        internal static List<Order?> Orderlist = new List<Order?>();
        internal static List<OrderItem?> OrderItemList = new List<OrderItem?>();
        internal static List<Product?> Productlist = new List<Product?>();

        static string[] customerAddress = { "beit shemesh", "jlm", "Beitar", "hebron", "tel aviv", "beit shemesh", "jlm", "Beitar", "hebron", "tel aviv" };
        static string[] customerName = { "sara", "rachelli", "tifferet", "gitty", "ahuva", "aryeh", "moshe", "shaya", "david", "yehoda" };
        static string[] productName = { "Bull Dog", "British Shorthair", "Cockatiel", "Holland Lop", "Gold Fish", "golden retriever", "Persian Cat", "Cockatoo", "Lionhead Rabbit", " Betta" };

        /// <summary>
        /// method to add an order to the order list
        /// </summary>
        /// <param name="orderlist"></param>
        /// <param name="o1"></param>
        private static void addOrder(Order o1)
        {
            Orderlist.Add(o1);
        }
        /// <summary>
        /// method to add an orderitem to the order item list
        /// </summary>
        /// <param name="orderitemlist"></param>
        /// <param name="oi1"></param>
        private static void addOrderItem(OrderItem oi1)
        {
            OrderItemList.Add(oi1);
        }
        /// <summary>
        /// method to add an product to the product list
        /// </summary>
        /// <param name="productlist"></param>
        /// <param name="p1"></param>
        private static void addProduct(Product p1)
        {
            Productlist.Add(p1);
        }

        /// <summary>
        /// adds products to the lists
        /// </summary>
        private static void s_Initialize()
        {
            // Order
            for (int i = 0; i < 10; i++)//adds 20 orders to the list
            {
                Order order = new Order();
                order.ID = config.GetOrderID;
                order.CustomerName = customerName[rand.Next(0, 10)];// generates random name
                order.CustomerAddress = customerAddress[rand.Next(0, 10)];//generats random location
                order.CustomerEmail = order.CustomerName + "@gmail.com";//creates email address
                order.OrderDate = DateTime.Now.Add(new TimeSpan(rand.Next(-360, 0), 0, 0, 0));//time in last year till 2 months ago
                if (i < 8)//80% have a ship date
                {
                    order.ShipDate = order.OrderDate?.Add(new TimeSpan(rand.Next(1, 7), 0, 0, 0));
                }
                else
                    order.ShipDate = null;

                if (i < 5)//60% have a delivery date
                {
                    order.DeliveryDate = order.ShipDate?.Add(new TimeSpan(rand.Next(1, 2), 0, 0, 0));//from one to 2 days later
                }
                else
                    order.ShipDate = null;
                Orderlist.Add(order);

            }// Product
            for (int i = 0; i < 10; i++)//adds ten products 
            {
                Product product = new Product();
                do
                {
                    product.ID = 300000 + i;
                }
                while (Productlist.Exists(x => x?.ID == product.ID));//makes sure there isn one with the same number
                product.Name = productName[i];//adds a name
                product.Price = (double)rand.Next(10, 200);//randome price from range 
                product.Category = Category.Dog + (i % 5);//assings in order 

                if (i > 2)//5% of products with no stock
                {
                    product.InStock = (int)rand.Next(1, 20);
                }
                else
                {
                    product.InStock = 0;
                }

                Productlist.Add(product);

            }
            //OterItem
            for (int j = 1; j < 20; j++)
            {
                for (int i = 0; i < 2; i++)//different number of products in order
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderItemID = config.GetOrderItemId;//id
                    Product p = Productlist[rand.Next(0, 9)].Value;//gives us a product
                    orderItem.Price = p.Price;//same price as product
                    orderItem.Amount = rand.Next(1, 10);//random amount
                    orderItem.ProductID = p.ID;
                    orderItem.OrderID = 100000 + j / 2;
                    OrderItemList.Add(orderItem);
                }

            }
        }

        #endregion



        /// <summary>
        /// default constructor
        /// </summary>
        static program()
        {
            s_Initialize();//in region
        }
  
        /// <summary>
        /// class to generat IDs
        /// </summary>
        internal static class config
        {
            private static int orderID = 100000;
            private static int orderItemId = 200000;
            public static int GetOrderID { get => orderID++; }
            public static int GetOrderItemId { get => orderItemId++; }
        }

        static void Main(string[] args)
        {
            //private static DalApi.IDal? dal = DalApi.Factory.Get();
            List<Order?> orderlist = Orderlist;
            List<OrderItem?> orderitemlist = OrderItemList;
            List<Product?> productlist = Productlist;
            string path = @"OrderItems.xml";
            string path2 = @"Product.xml";
            string path1 = @"Orders.xml";
          //  XMLTools.SaveListToXML<Order?>(orderlist, path1);
          
          // XMLTools.SaveListToXML<OrderItem?>(orderitemlist, path);
           
          //  XMLTools.SaveListToXML<Product?>(productlist, path2);
            Console.WriteLine("jsj");
            //List<Product> list2 = XMLTools.LoadListFromXML<Product>(path2);    
        }
    }
}