using BO;
namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// returns the list of orders- for the admin
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderForList?> GetOrderList(); 
    /// <summary>
    /// returns an order -for admin and user
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Order GetOrderInfo(int orderId);
    /// <summary>
    /// updates the shipping date - for the admin
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Order UpdateShippingDate(int orderId);
    /// <summary>
    /// updates the delivery date - for the admin
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Order UpdateDeliveryDate(int orderId);
    /// <summary>
    /// the function gets an order id and returns the status of the order- for admin
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.OrderTracking OrderStatus(int orderId);
    
    /// <summary>
    /// BONUS!!
    /// </summary>
    /// <param name="order"></param>
    public void UpdateOrder(BO.Order order, int newAmount,BO.OrderItem orderItem1);

}
