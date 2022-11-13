using DO;
using DalApi;
using System.Runtime.Serialization;

namespace Dal;

public class DalOrderItem:IOrderItem
{
    /// <summary>
    /// method gets an order item  and adds to list and returns the id
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem orderItem)
    {
        orderItem.OrderItemID = DataSource.config.GetOrderItemId;
        foreach (OrderItem item in DataSource.OrderItemList)
        {
            if(item.OrderItemID == orderItem.OrderItemID)
                throw new Exception("OrderItem already exist");
        }
        Random r1 = new Random();
        Random r2 = new Random();
        orderItem.ProductID = r1.Next(300000, 300020);//generates the ids
        orderItem.OrderID = r2.Next(100000, 100010);

        DataSource.OrderItemList.Add(orderItem);
        return orderItem.OrderItemID;
    }
    /// <summary>
    /// method gets an order item ID and prints renurns the order item  it belongs to
    /// </summary>
    /// <param name="orderitemid"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem Get(int orderitemid)
    {
        
        foreach (OrderItem item in DataSource.OrderItemList)//goes through the list looking for the order.
        {
            if (item.OrderItemID == orderitemid )
                return item;
        }

        throw new Exception("order item does not exist");
       
    }
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
    public void Update(OrderItem orderItem)
    {
        int count = 0;
        foreach (OrderItem item in DataSource.OrderItemList)//goes through the list looking for the order.
        {
            if (item.OrderItemID != orderItem.OrderItemID) count++;
            if (item.OrderItemID == orderItem.OrderItemID)
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
    public List<OrderItem> GetAll() => DataSource.OrderItemList;
}
