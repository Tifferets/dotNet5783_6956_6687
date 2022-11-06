using DO;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem orderItem)
    {
        foreach(OrderItem item in DataSource.OrderItemList)
        {
            if(item.OrderID == orderItem.OrderID || item.ProductID == orderItem.ProductID)
                throw new Exception("OrderItem already exist");
        }
        //product.ID = DataSource.config.GetProductID;//gets a generated id from data source inner class
        //return product.ID;
    }
    public OrderItem Get(int productID,int orderID)
    {
        //try
        //{
        foreach (OrderItem item in DataSource.OrderItemList)//goes through the list looking for the order.
        {
            if (item.OrderID == orderID && item.ProductID == productID)
                return item;
        }

        throw new Exception("Product does not exist");
        // }
        // catch(Exception ex)   { Console.WriteLine(ex); }
    }
    public void Delete(int productID,int orderID)
    {
        foreach (OrderItem item in DataSource.OrderItemList)//goes through the list looking for the order.
        {
            if (item.OrderID == orderID && item.ProductID == productID)
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
            if (item.OrderID!= orderItem.OrderID|| item.ProductID == orderItem.ProductID) count++;
            if (item.OrderID == orderItem.OrderID&& item.ProductID == orderItem.ProductID)
            {
                DataSource.OrderItemList[count] = orderItem;
                break;
            }
        }
    }
}
