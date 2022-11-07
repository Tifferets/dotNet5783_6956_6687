using System.ComponentModel;
using System.Xml;
using DO;

namespace Dal;

public class DalOrder
{
    /// <summary>
    /// method that gets an order,then id from config and adds to the list
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public int Add(Order order)
    {
       order.ID = DataSource.config.GetOrderID;//gets a generated id from data source inner class
       DataSource.Orderlist.Add(order);//not recursion
       return order.ID;
    }
    /// <summary>
    /// method gets an order ID and prints renurns the order it belongs to
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order Get(int orderID)
    {
       
           foreach(Order item in DataSource.Orderlist)//goes through the list looking for the order.
           {
                if(item.ID == orderID)  
                    return item;
           }
           throw new Exception("order does not exist");
       
    }
    public void Delete(int orderID)
    {
        foreach (Order item in DataSource.Orderlist)//goes through the list looking for the order.
        {
            if (item.ID == orderID)
            { 
                DataSource.Orderlist.Remove(item);
                break; 
            }
        }
    }
    public void Update(Order order)
    {
        int count = 0;
        foreach (Order item in DataSource.Orderlist)//goes through the list looking for the order.
        {
            if (item.ID != order.ID) count++;
            if (item.ID==order.ID)
            {
                DataSource.Orderlist[count] = order;
                break; 
            }
        }
    }
    public List<Order> GetAll() => DataSource.Orderlist;

}
