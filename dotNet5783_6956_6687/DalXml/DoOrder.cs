using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

public class DoOrder:IOrder
{
    static string fPath = @"Order";
    static string orderItemPath = @"OrderItem";
    XElement configRoot;
    string configPath = @"Config.xml";
    private void LoadData()
    {
        try
        {
            configRoot= XElement.Load(configPath);
        }
        catch(Exception ex)
        {
            throw new Exception("cant load");
        }
    }
    private void SaveData(int orderID)
    {
        try
        {
            configRoot = new XElement("ID", new XElement("OrderID", orderID));
            configRoot.Save(configPath);
        }
        catch(Exception ex) { throw new Exception("cant save"); }
    }
    //public static void SaveListToXML(List<Order> listOfOrders, string path)
    //{
    //    FileStream fs = new FileStream(path, FileMode.Create);
    //    XmlSerializer x =new XmlSerializer(listOfOrders.GetType());
    //    x.Serialize(fs, listOfOrders);
    //}
    //public static List<Order> LoadListFromXML(string path)
    //{
    //    FileStream fs = new FileStream(path, FileMode.Open);
    //    XmlSerializer x = new XmlSerializer(typeof(List<Order>));
    //    return (List<Order>)x.Deserialize(fs);
    //}


    /// <summary>
    /// method that gets an order,then id from config and adds to the list
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public int Add(Order order)
    {
        List<DO.Order> OrderList =Dal.XMLTools.LoadListFromXML<DO.Order>(fPath); //gets all the orders
        LoadData();
        try
        {
            XElement orderid = configRoot.Element("OrderID");
            int numid = Convert.ToInt32(orderid.Element("OrderID").Value);
            order.ID = numid++;
            OrderList.Add(order);
            SaveData((int)orderid);
            Dal.XMLTools.SaveListToXML(OrderList, fPath);
            return order.ID;
        }
        catch (Exception ex)
        {
            throw new Exception("couldnt add");
        }
        //order.ID = DataSource.config.GetOrderID;//gets a generated id from data source inner class
        //DataSource.Orderlist.Add(order);//not recursion
       // return order.ID;

    }

    /// <summary>
    /// method gets an order ID and renurns the order it belongs to
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    //public Order Get(int orderID)
    //{

    //       foreach(Order item in DataSource.Orderlist)//goes through the list looking for the order.
    //       {
    //            if(item.ID == orderID)  
    //                return item;
    //       }
    //       throw new Exception("order does not exist");

    //}
    /// <summary>
    /// method gets an order ID and delets the right order
    /// </summary>
    /// <param name="orderID"></param>
    public void Delete(int orderID)
    {//gets an orders id and deletes the order

        LoadData();
        List<DO.Order> OrderList = Dal.XMLTools.LoadListFromXML<DO.Order>(fPath); //gets all the orders
        Order or = OrderList.FirstOrDefault(x => x.ID == orderID);//or is A copy of the order we  want to delete
        OrderList.Remove(or);//removes the order from the list
        Dal.XMLTools.SaveListToXML(OrderList, fPath);//saves the list into xml file

    }
    /// <summary>
    /// method gets an order and updates its details
    /// </summary>
    /// <param name="order"></param>
    public void Update(Order order)
    {//gets an order in its updated version and updates it in the list
        
        try
        {
            LoadData();
            List<DO.Order> OrderList = Dal.XMLTools.LoadListFromXML<DO.Order>(fPath); //gets all the orders
            OrderList.Remove(order);//deletes the order from the list
            OrderList.Add(order);
            OrderList = OrderList.OrderBy(x => x.ID).ToList();// sorts the list by  id 
            Dal.XMLTools.SaveListToXML(OrderList, fPath);//saves the list into xml file
        }
        catch(Exception ex)
        {
            throw new Exception("cant update");
        }
    }

    public IEnumerable<OrderItem?> GetAllOrderItems(int id)//returns all the order items for the spicific order by its id
    {
        try
        {
            LoadData();
            List<DO.Order> OrderList = Dal.XMLTools.LoadListFromXML<DO.Order>(fPath); //gets all the orders
           

            Dal.XMLTools.SaveListToXML(OrderList, fPath);//saves the list into xml file
        }
        catch (Exception ex)
        {
            throw new Exception("cant update");
        }



        List<OrderItem?> lst = (from OrderItem? item in DataSource.OrderItemList ?? throw new NullException()
                                where item?.OrderID == id
                                select item).ToList();
        //List<OrderItem?> lst = new List<OrderItem?>();
        //foreach (OrderItem? item in DataSource.OrderItemList ?? throw new NullException())
        //{
        //    if (item?.OrderItemID == id)
        //    {
        //        lst.Add(item);
        //    }
        //}
        //IEnumerable<OrderItem?> orderItems = lst;
        return lst;
    }
    /// <summary>
    /// method returns the list 
    /// </summary>
    /// <returns></returns>
    //public IEnumerable<Order?> GetAll() => DataSource.Orderlist;
    /// <summary>
    /// returns list of all order items with the id
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func)
    {
        if (func is null)
        {
            return XMLTools.LoadListFromXML<Order?>(fPath);
        }
        else
        {
            return XMLTools.LoadListFromXML<Order?>(fPath).Where(func);
        }
    }
    public Order? GetSingle(Func<Order?, bool>? func)
    {
        try 
        {
            LoadData();
            List<DO.Order> OrderList = Dal.XMLTools.LoadListFromXML<DO.Order>(fPath); //gets all the orders
            Order? or = OrderList.FirstOrDefault(x => func(x));//or is the order were looking for
            Dal.XMLTools.SaveListToXML(OrderList, fPath);//saves the list into xml file
            return or;
        }
        catch(Exception ex)
        {
            throw new Exception("cant get a single order");
        }

    }
    //DataSource.Orderlist.FirstOrDefault((func ?? throw new NullException())); // return an order with this id

}
