using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

public class DoOrder:IOrder
{
    static string dir = @"..\xml\";
    static string fPath = @"Orders.xml";
    static string orderItemPath = @"OrderItems.xml";


    /// <summary>
    /// method that gets an order,then id from config and adds to the list
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public int Add(Order order)
    {
        List<DO.Order> OrderList = Dal.XMLTools.LoadListFromXML<DO.Order>(dir+fPath); //gets all the orders
        try
        {
            int orderid = Config.getNewOrderID();//gets an id from config
            order.ID = orderid;
            OrderList.Add(order);//adds to list
            Dal.XMLTools.SaveListToXML(OrderList, fPath);
            return order.ID;
        }
        catch (Exception ex)
        {
            throw new errorException();
        }


    }


    /// <summary>
    /// method gets an order ID and delets the right order
    /// </summary>
    /// <param name="orderID"></param>
    public void Delete(int orderID)
    {//gets an orders id and deletes the order
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
            List<DO.Order> OrderList = Dal.XMLTools.LoadListFromXML<DO.Order>(fPath);//gets all the orders
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
            List<DO.OrderItem?> OrderItemsList = Dal.XMLTools.LoadListFromXML<DO.OrderItem?>(dir+orderItemPath); //gets all the orderitem
            List<DO.OrderItem?> items = OrderItemsList.Where(x =>x?.OrderID == id).ToList();//items is a list of all the orderitems that have order id is the id we got


            Dal.XMLTools.SaveListToXML(OrderItemsList, orderItemPath);//saves the list into xml file
            return items;//returns the list of all the order items with the order id
        }
        catch (Exception ex)
        {
            throw new Exception("cant update");
        }
    }
    /// <summary>
    /// returns list of all order items with the id
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func)
    {
        if (func is null)
        {
            IEnumerable<Order?> lst= XMLTools.LoadListFromXML<Order?>(fPath);
            return lst;// XMLTools.LoadListFromXML<Order?>(fPath);
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
}
