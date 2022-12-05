using DalApi;
using DO;
using System.Linq;

namespace Dal;

internal class DalOrder: IOrder
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
    /// <summary>
    /// method gets an order and updates its details
    /// </summary>
    /// <param name="order"></param>
    public void Update(Order? order)
    {
        int count = 0;
        foreach (Order item in DataSource.Orderlist)//goes through the list looking for the order.
        {
            if (item.ID != order.Value.ID) count++;
            if (item.ID==order.Value.ID)
            {
                DataSource.Orderlist[count] = order;
                break; 
            }
        }
    }
    public IEnumerable<OrderItem?> GetAllOrderItems(int id)
    {
        List<OrderItem?> lst=new List<OrderItem?>();
        foreach(OrderItem item in DataSource.OrderItemList)
        {
            if(item.OrderItemID == id)
            {
                lst.Add(item);
            }
        }
        IEnumerable<OrderItem?> orderItems = lst;
        return orderItems;
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
        if (func == null)
        {
            return DataSource.Orderlist;//if null retun te whole list
        }
        List<Order?> result = new List<Order?>();
        foreach (var item in DataSource.Orderlist)
        {
            if (func(item))//if the id is good
                result.Add(item);//adds to list 
        }
        return result;
    }
   public Order? GetSingle(Func<Order?, bool>? func) => DataSource.Orderlist.First(func); // return an order with this id
}
