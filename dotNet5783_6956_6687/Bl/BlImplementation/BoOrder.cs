using BlApi;

namespace BlImplementation;

internal class BoOrder:IOrder
{
    private static DalApi.IDal dalList = new Dal.DalList();

    /// <summary>
    /// returns the list of orders- for the admin
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        IEnumerable<BO.OrderForList> OrderForlist = new List<BO.OrderForList>();
        try
        {
            foreach(DO.Order item in dalList.order.GetAll())
            {
                foreach(DO.OrderItem oitem in dalList.order.GetAllOrderItems())
                {
                    
                }
            }
        }
        catch
        {
            throw new BO.errorException();
        }
    }
    /// <summary>
    /// returns an order -for admin and user
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order GetOrderInfo(int orderId)
    {
        if(orderId > 0)
        {
            try
            {
                BO.Order newOrder = new BO.Order();
                foreach(DO.Order item in dalList.order.GetAll())
                {
                    if(item.ID == orderId)
                    {
                        item.
                        newOrder.CustomerName = item.CustomerName;
                        newOrder.CustomerAddress = item.CustomerAddress;
                        newOrder.CustomerEmail = item.CustomerEmail;
                        newOrder.OrderDate = item.OrderDate;
                        newOrder.PaymentDate= item.

                    }
                }

            }
            catch
            {

            }
        }
    }
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
    public IEnumerable<Order> OrderStatus(int orderId);
    /// <summary>
    /// BONUS!!
    /// </summary>
    /// <param name="order"></param>
    public void UpdateOrder(Order order);

}

