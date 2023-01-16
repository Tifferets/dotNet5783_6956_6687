using DO;
using DalApi;
using System.Runtime.Serialization;
using System.Linq;

namespace Dal;

internal class DalOrderItem:IOrderItem 
{
    /// <summary>
    /// method gets an order item  and adds to list and returns the id
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem  orderItem)
    {
        orderItem.OrderItemID = DataSource.config.GetOrderItemId;
        if((DataSource.OrderItemList ?? throw new NullException()).Contains(orderItem))//checks if it existes 
                throw new Exception("OrderItem already exist");
        Random r1 = new Random();
        Random r2 = new Random();
        orderItem.ProductID = r1.Next(300000, 300020);//generates the ids
        orderItem.OrderID = r2.Next(100000, 100010);
        DataSource.OrderItemList.Add(orderItem);
        return orderItem.OrderItemID;
    }
  
    /// <summary>
    /// method gets an order item id and delets the right oder item
    /// </summary>
    /// <param name="orderItemid"></param>
    public void Delete(int orderItemid)
    {
        (DataSource.OrderItemList ?? throw new NullException()).Remove(GetSingle(x => x.Value.OrderItemID == orderItemid));// deletes the order item
       
    }
    /// <summary>
    /// method gets an order item and updates its details
    /// </summary>
    /// <param name="orderItem"></param>
    public void Update(OrderItem orderItem)
    {
        Delete(orderItem.OrderItemID);//deletes it
        DataSource.OrderItemList.Add(orderItem);//adds it
        DataSource.OrderItemList =DataSource.OrderItemList.OrderByDescending(x=> -x?.OrderItemID).ToList(); //sorts by id 
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
            return DataSource.OrderItemList ?? throw new NullException();//if null retun te whole list
        }
        var result = (from OrderItem? item in DataSource.OrderItemList ?? throw new NullException()
                      where func(item)
                      select item);
        return result;
    }
    public OrderItem? GetSingle(Func<OrderItem?, bool>? func) => DataSource.OrderItemList.First(func ?? throw new NullException()); // return a product with this id

}
