//using DO;
//using System.ComponentModel;
//using System.Data;

//namespace Dal;

//internal static class DataSource
//{
//    public static readonly int random;// not sure about random number...
//    internal static List<Order> Orderlist = new List<Order>();
//    internal static List<OrderItem> OrderItemList = new List<OrderItem>();
//    internal static List<Product> Productlist = new List<Product>();

//    static string[] customerAddress = {"beit shemesh", "jlm" };
//    static string[] customerName = { "sara", "rachelli" };
//    static string[] customerEmail = { "sara@gmail.com", "rachelli@gmail.com" };
//    static string[] productName = { "gray puppy", "cute cat" };
//    static double[] productPrice = { 20, 30 };
//    static string[] productCategory = { "dog", "cat" };
//    static int[] productInStock = { 1, 2 };
//    static int[] orderItemAmount = { 1, 2 };


//    /// <summary>
//    /// method to add an order to the order list
//    /// </summary>
//    /// <param name="orderlist"></param>
//    /// <param name="o1"></param>
//    private static void addOrder( Order o1)
//    {
//        Orderlist.Add(o1);
//    }
//    /// <summary>
//    /// method to add an orderitem to the order item list
//    /// </summary>
//    /// <param name="orderitemlist"></param>
//    /// <param name="oi1"></param>
//    private static void addOrderItem(OrderItem oi1)
//    {
//        OrderItemList.Add(oi1);
//    }
//    /// <summary>
//    /// method to add an product to the product list
//    /// </summary>
//    /// <param name="productlist"></param>
//    /// <param name="p1"></param>
//    private static void addProduct( Product p1)
//    {
//        Productlist.Add(p1);
//    }

//    /// <summary>
//    /// default constructor
//    /// </summary>
//    static DataSource()
//    {
//        s_Initialize();
//    }

//    private static void s_Initialize()
//    {
//        for (int i = 0; i < 2; i++)
//        {
//            Order order = new Order();
//            order.CustomerAddress = customerAddress[i];
//            order.CustomerEmail = customerEmail[i];
//            order.CustomerName= customerName[i];
//            order.OrderDate= DateTime.MinValue;
//            TimeSpan ts = new TimeSpan(2);
//            order.ShipDate = order.OrderDate + ts;
//            order.DeliveryDate = order.ShipDate + ts;
//            Add(order);// ???
//        }
//        for (int i = 0; i < 2; i++)
//        {
//            Product product = new Product();
//            product.Name = productName[i];
//            product.Price= productPrice[i];
//            product.Category= productCategory[i];
//            product.InStock = productInStock[i];
//            Add(product);
//        }
//        for(int i = 0; i < 2; i++)
//        {
//            OrderItem orderItem = new OrderItem();
//            orderItem.Price= productPrice[i];
//            orderItem.Amount = orderItemAmount[i];
//            Add(orderItem);
//        }
//    }
//    internal static class config
//    {
//        static int orderID = 100000;
//        static int productID = 500000;
//        public static int GetOrderID { get => orderID++; }
//        public static int GetProductID { get => productID++; } 
        

//    }
//}
