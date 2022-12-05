using DO;
using DalApi;
using System.Runtime.Serialization;

namespace Dal;

internal class DalOrderItem:IOrderItem // internal?
{
    /// <summary>
    /// method gets an order item  and adds to list and returns the id
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem? orderItem)
    {

        orderItem.OrderItemID = DataSource.config.GetOrderItemId;
        foreach (OrderItem item in DataSource.OrderItemList)
        {
            if(item.OrderItemID == orderItem.Value.OrderItemID)
                throw new Exception("OrderItem already exist");
        }
        Random r1 = new Random();
        Random r2 = new Random();
        orderItem.ProductID = r1.Next(300000, 300020);//generates the ids
        orderItem.OrderID = r2.Next(100000, 100010);

        DataSource.OrderItemList.Add(orderItem);
        return orderItem.Value.OrderItemID;
    }
    /// <summary>
    /// method gets an order item ID and prints renurns the order item  it belongs to
    /// </summary>
    /// <param name="orderitemid"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    //public OrderItem Get(int orderitemid)
    //{
        
    //    foreach (OrderItem item in DataSource.OrderItemList)//goes through the list looking for the order.
    //    {
    //        if (item.OrderItemID == orderitemid )
    //            return item;
    //    }

    //    throw new Exception("order item does not exist");
       
    //}
    /// <summary>
    /// method gets an order item id and delets the right oder item
    /// </summary>
    /// <param name="orderItemid"></param>
    public void Delete(int orderItemid)
    {
        foreach (OrderItem item in DataSource.OrderItemList)//goes through the list looking for the order.
        {
            if (item.OrderItemID ==orderItemid)
            {
                DataSource.OrderItemList.Remove(item);
                break;
            }
        }
    }
    /// <summary>
    /// method gets an order item and updates its details
    /// </summary>
    /// <param name="orderItem"></param>
    public void Update(OrderItem? orderItem)
    {
        int count = 0;
        foreach (OrderItem item in DataSource.OrderItemList)//goes through the list looking for the order.
        {
            if (item.OrderItemID != orderItem?.OrderItemID) count++;
            if (item.OrderItemID == orderItem?.OrderItemID)
            {
                DataSource.OrderItemList[count] = orderItem;
                break;
            }
        }
    }
    /// <summary>
    /// method returns the list 
    /// </summary>
    /// <returns></returns>
    //public IEnumerable<OrderItem?> GetAll() => DataSource.OrderItemList;//changed to IEnumerable!!!!!!!!!!!!!
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func)
    {
        if (func == null)
        {
            return DataSource.OrderItemList;//if null retun te whole list
        }
        List<OrderItem?> result = new List<OrderItem?>();
        foreach (var item in DataSource.OrderItemList)
        {
            if (func(item))//if the id is good
                result.Add(item);//adds to list 
        }
        return result;
    }
    public OrderItem? GetSingle(Func<OrderItem?, bool>? func) => DataSource.OrderItemList.First(func); // return a product with this id

}
