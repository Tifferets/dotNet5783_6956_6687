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

public class DoOrderItem:IOrderItem
{
    const string orderItem = "OrderItem";
    public int Add(OrderItem OrderItem)
    {
       
        try
        {
            List<OrderItem> OrderItemList = Dal.XMLTools.LoadListFromXML<OrderItem>(orderItem); //gets all the orders                                                                                             
            int orderItemId = Config.getNewOrderItemID();
            // int numid = Convert.ToInt32(orderid.Element("OrderID").Value);
            OrderItem.OrderItemID = orderItemId;
            OrderItemList.Add(OrderItem);
            //SaveData((int)orderid);
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
    public void Update(OrderItem OrderItem)//gets an orderite and updates it in the list
    {

        try
        {
            List<DO.OrderItem> OrderItemList = Dal.XMLTools.LoadListFromXML<DO.OrderItem>(orderItem);//gets all the orders
            OrderItemList.Remove(OrderItem);//deletes the order from the list
            OrderItemList.Add(OrderItem);
            OrderItemList = OrderItemList.OrderBy(x => x.OrderItemID).ToList();// sorts the list by  id 
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
            return XMLTools.LoadListFromXML<OrderItem?>(orderItem);
        }
        else 
        {
            return XMLTools.LoadListFromXML<OrderItem?>(orderItem).Where(func);
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
