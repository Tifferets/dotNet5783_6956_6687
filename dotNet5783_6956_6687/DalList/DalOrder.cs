using System.ComponentModel;
using System.Xml;
using DO;

namespace Dal;

public class DalOrder
{
    public int Add(Order order)
    {
       order.ID = DataSource.config.GetOrderID;//gets a generated id from data source inner class
        return order.ID;
    }
    public Order Get(int orderID)
    {
        //try
        //{
           foreach( Order item in DataSource.Orderlist)//goes through the list looking for the order.
           {
                if(item.ID == orderID)  
                    return item;
           }
       
        throw new Exception("order does not exist");
       // }
       // catch(Exception ex)   { Console.WriteLine(ex); }
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
    
}
