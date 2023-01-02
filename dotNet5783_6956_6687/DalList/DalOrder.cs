using DalApi;
using DO;
using System.Linq;

namespace Dal;

internal class DalOrder : IOrder
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
        DataSource.Orderlist.Remove(GetSingle(x=> x?.ID== orderID));
        //foreach (Order? item in DataSource.Orderlist)//goes through the list looking for the order.
        //{
        //    if (item?.ID == orderID)
        //    {
        //        DataSource.Orderlist.Remove(item);
        //        break;
        //    }
        //}
    }
    /// <summary>
    /// method gets an order and updates its details
    /// </summary>
    /// <param name="order"></param>
    public void Update(Order order)
    {
        Delete(order.ID);

        DataSource.Orderlist.Add(order);
        DataSource.Orderlist = DataSource.Orderlist.OrderByDescending(x => -x?.ID).ToList();// sorts the list by small id to bigger id
        //int count = 0;
        //foreach (Order? item in (DataSource.Orderlist ?? throw new NullException()))//goes through the list looking for the order.
        //{
        //    if (item?.ID != order.ID) count++;
        //    if (item?.ID==order.ID)
        //    {
        //        DataSource.Orderlist[count] = order;
        //        break;
        //    }
        //}
    }
    public IEnumerable<OrderItem?> GetAllOrderItems(int id)//returns all the order items for the spicific order by its id
    {
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
        if (func == null)
        {
            return (DataSource.Orderlist ?? throw new NullException());//if null retun te whole list
        }

        return (from item in (DataSource.Orderlist ?? throw new NullException())
                where func(item)//if the id is good
                select item).ToList()//adds to list 
;
        //List<Order?> result = new List<Order?>();
        //foreach (var item in (DataSource.Orderlist ?? throw new NullException()))
        //{
        //    if (func(item))//if the id is good
        //        result.Add(item);//adds to list 
        //}
        //return result;
    }
    public Order? GetSingle(Func<Order?, bool>? func)=> DataSource.Orderlist.FirstOrDefault((func ?? throw new NullException())); // return an order with this id
      
    
    
}
