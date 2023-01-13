using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
namespace Dal;

public class DoOrderItem : IOrderItem
{
    static string dir = @"..\xml\";
    const string orderItem = @"OrderItems.xml";
    public int Add(OrderItem OrderItem)
    {
        try
        {
            List<OrderItem> OrderItemList = Dal.XMLTools.LoadListFromXML<OrderItem>(orderItem); //gets all the orders                                                                                             
            int orderItemId = Config.getNewOrderItemID();//gets id from the 
            OrderItem.OrderItemID = orderItemId;
            OrderItemList.Add(OrderItem);//adds to list
            Dal.XMLTools.SaveListToXML(OrderItemList, orderItem);
            return OrderItem.OrderItemID;
        }
        catch
        {
            throw new Exception("couldnt add");
        }
    }
    public void Delete(int orderitemID)//gets an orderitem id and deletes the orderitem
    {
        List<OrderItem> OrderItemList = Dal.XMLTools.LoadListFromXML<DO.OrderItem>(orderItem); //gets all the orders
        OrderItem or = OrderItemList.FirstOrDefault(x => x.OrderItemID == orderitemID);//or is A copy of the order we  want to delete
        OrderItemList.Remove(or);//removes the order from the list
        Dal.XMLTools.SaveListToXML(OrderItemList, orderItem);//saves the list into xml file

    }
    public void Update(OrderItem orderItem1)//gets an orderite and updates it in the list
    {
        try
        {
            List<DO.OrderItem> OrderItemList = Dal.XMLTools.LoadListFromXML<DO.OrderItem>(orderItem);//gets all the orders
            int index = OrderItemList.FindIndex(t => t.OrderItemID == orderItem1.OrderItemID);
            if (index == -1)
                throw new Exception("DAL: Order Item with the same id not found...");
            OrderItemList[index] = orderItem1;
            Dal.XMLTools.SaveListToXML(OrderItemList, orderItem);//saves the list into xml file
        }
        catch (Exception ex)
        {
            throw new Exception("cant update");
        }
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        if (func is null)
        {
            return XMLTools.LoadListFromXML<OrderItem?>(orderItem);//returns the whole list
        }
        else
        {
            return XMLTools.LoadListFromXML<OrderItem?>(orderItem).Where(func);//returns all that are tre from the func
        }

    }
    public OrderItem? GetSingle(Func<OrderItem?, bool>? func)
    {
        try
        {
            List<DO.OrderItem> OrderItemList = Dal.XMLTools.LoadListFromXML<DO.OrderItem>(orderItem); //gets all the orders
            OrderItem? or = OrderItemList.FirstOrDefault(x => func(x));//or is the order were looking for
            Dal.XMLTools.SaveListToXML(OrderItemList, orderItem);//saves the list into xml file
            return or;
        }
        catch (Exception ex)
        {
            throw new Exception("cant get a single order");
        }

    }
}
