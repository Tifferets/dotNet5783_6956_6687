using DO;
using System.ComponentModel;
using System.Data;

namespace Dal;

internal static class DataSource
{
    public static readonly Random rand = new Random();//to generat random 
    internal static List<Order?> Orderlist = new List<Order?>();
    internal static List<OrderItem?> OrderItemList = new List<OrderItem?>();
    internal static List<Product?> Productlist = new List<Product?>();

    static string[] customerAddress = { "beit shemesh", "jlm", "Beitar","hebron","tel aviv", "beit shemesh", "jlm", "Beitar", "hebron", "tel aviv" };
    static string[] customerName = { "sara", "rachelli", "tifferet", "gitty", "ahuva", "aryeh", "moshe", "shaya", "david", "yehoda" };
    static string[] productName = { "Product 1", "Product 2", "Product 3", "Product 4", "Product 5", "Product 6", "Product 7", "Product 8", "Product 9", "Product 10" };
    
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
    /// default constructor
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }
    /// <summary>
    /// adds products to the lists
    /// </summary>
    private static void s_Initialize()
    {
        // Order
        for (int i = 0; i <10 ; i++)//adds 20 orders to the list
        {
            Order order = new Order();
            order.ID = config.GetOrderID;
            order.CustomerName = customerName[rand.Next(0,10)];// generates random name
            order.CustomerAddress = customerAddress[rand.Next(0, 10)];//generats random location
            order.CustomerEmail = order.CustomerName + "@gmail.com";//creates email address
            order.OrderDate = DateTime.Now.Add(new TimeSpan(rand.Next(-360, 0), 0, 0, 0));//time in last year till 2 months ago
            if(i < 16)//80% have a ship date
            { 
                order.ShipDate = order.OrderDate.Value.Add(new TimeSpan(rand.Next(1, 7), 0, 0, 0));
            }
            else
                order.ShipDate = DateTime.MinValue;

            if (i <10 )//60% have a delivery date
            {
                order.DeliveryDate = order.ShipDate.Value.Add(new TimeSpan(rand.Next(1, 2), 0, 0, 0));//from one to 2 days later
            }
            else
                order.ShipDate = DateTime.MinValue;
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
            product.Category = (Category)rand.Next(0, 4);

            if (i > 1)//5% of products with no stock
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
        for(int j=0; j < 20; j++)
        { 
            for (int i = 0; i < 2; i++)//different number of products in order
            {
                OrderItem orderItem = new OrderItem();
                orderItem.OrderItemID = config.GetOrderItemId;//id
                Product p = Productlist[rand.Next(0, 9)].Value;//gives us a product
                orderItem.Price = p.Price;//same price as product
                orderItem.Amount = (int)rand.Next(1,10);//randme amount
                orderItem.ProductID = p.ID;
                orderItem.OrderID = 100000 + j;
                OrderItemList.Add(orderItem);
            }
            
        }
    }
    /// <summary>
    /// class to generat IDs
    /// </summary>
    internal static class config
    {
        static int orderID = 100000;
        static int orderItemId = 200000;
        public static int GetOrderID { get => orderID++; }
        public static int GetOrderItemId { get => orderItemId++; }

    }
}


