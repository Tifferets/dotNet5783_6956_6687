using DO;
using System.ComponentModel;

namespace Dal;

internal static class DataSource
{
    internal static class Config 
    {
        
    }
    public static readonly int random;// not sure about random number...
    internal static List<Order> Orderlist = new List<Order>();
    internal static List<OrderItem> OrderItemList = new List<OrderItem>();
    internal static List<Product> Productlist = new List<Product>();
    /// <summary>
    /// method to add an order to the order list
    /// </summary>
    /// <param name="orderlist"></param>
    /// <param name="o1"></param>

    private static void addOrder(ref List<Order> orderlist,int ID,string customerName,string customerEmail,string customerAddress,DateTime orderDate,DateTime shipDate,DateTime deliveryDate )
    {
        Order order = new Order();
        order.ID = ID;
        order.CustomerName = customerName;  
        order.CustomerEmail = customerEmail;    
        order.CustomerAddress = customerAddress;    
        order.OrderDate = orderDate;    
        order.DeliveryDate = deliveryDate;  
        order.ShipDate= shipDate;   

        orderlist.Add(order);   //adds the order
    }
    //private static void addOrder(ref List<Order> orderlist, Order o1)
    //{
    //    orderlist.Add(o1);
    //}

    /// <summary>
    /// method to add an orderitem to the order item list
    /// </summary>
    /// <param name="orderitemlist"></param>
    /// <param name="oi1"></param>
    private static void addOrderItem(ref List<OrderItem> orderitemlist, int productID, int orderID,double price,int amount) 
    {
        OrderItem  oi= new OrderItem();
        oi.ProductID = productID;
        oi.OrderID = orderID;
        oi.Price= price;
        oi.Amount= amount;
        orderitemlist.Add(oi);//adds the orderitem to the list
    }
    //private static void addOrderItem(ref List<OrderItem> orderitemlist, OrderItem oi1)
    //{
    //    orderitemlist.Add(oi1);
    //}
    /// <summary>
    /// method to add an product to the product list
    /// </summary>
    /// <param name="productlist"></param>
    /// <param name="p1"></param>
    private static void addProduct(ref List<Product> productlist, string name, int id , double price, string category , int instock)
    {
        //productlist.Add(p1);
        Product newproduct = new Product();
        newproduct.ID = id; 
        newproduct.Name = name;
        newproduct.Price = price;
        newproduct.Category = category;
        newproduct.InStock= instock;
        productlist.Add(newproduct);

    }
    private static void s_Initialize()
    {

    }
}
