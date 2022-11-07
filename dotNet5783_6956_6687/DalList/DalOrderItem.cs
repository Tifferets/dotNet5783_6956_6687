using DO;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem orderItem)
    {
        foreach(OrderItem item in DataSource.OrderItemList)
        {
            if(item.OrderItemID == orderItem.OrderItemID)
                throw new Exception("OrderItem already exist");
        }
        //product.ID = DataSource.config.GetProductID;//gets a generated id from data source inner class
        return orderItem.OrderItemID;
    }
    public OrderItem Get(int orderitemid)
    {
        //try
        //{
        foreach (OrderItem item in DataSource.OrderItemList)//goes through the list looking for the order.
        {
            if (item.OrderItemID == orderitemid )
                return item;
        }

        throw new Exception("Product does not exist");
        // }
        // catch(Exception ex)   { Console.WriteLine(ex); }
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
}
