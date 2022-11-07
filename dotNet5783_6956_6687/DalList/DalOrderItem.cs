using DO;

namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// gets an id and adds to list, 
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
        orderItem.ProductID = r1.Next(300000, 300020);
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
    public List<OrderItem> GetAll() => DataSource.OrderItemList;
}
