using System;
using System.Collections.Generic;
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

    static OrderItem? GetOrderItem(XElement oi) =>
        oi.Element("OrderItemID") is null ? null : new OrderItem()
        {
         OrderItemID= (int)oi.Element("OrderItemID")!,
         OrderID = (int)oi.Element("OrderID")!,
         ProductID= (int)oi.Element("OrderItemID")!,
         Price = (double)oi.Element("Price")!,
         Amount = (int)oi.Element("Amount")!,
        };
}
