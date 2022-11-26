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
        List<BO.OrderForList> OrderForlist = new List<BO.OrderForList>();//list of orderForList
        try
        {
            foreach(DO.Order item in dalList.order.GetAll())
            {
                
                BO.OrderForList ofl = new BO.OrderForList();
                ofl.ID= item.ID;
                ofl.CustomerName= item.CustomerName;
                ofl.Status= BO.OrderStatus.shipped;
                double? price = 0;
                int amount = 0;
                foreach(DO.OrderItem oitem in dalList.order.GetAllOrderItems())//loop to count the amount of products and total price
                {
                    amount++;
                    price += oitem.Price;
                }
                ofl.TotalPrice = price;
                ofl.AmountOfItems = amount;
                OrderForlist.Add(ofl);
            }
            IEnumerable<BO.OrderForList> orderForLists = OrderForlist;//list to return
            return orderForLists;
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
                foreach(DO.Order item in dalList.order.GetAll())//loop to go over all the orders
                {
                    if(item.ID == orderId)//checking if the id is the same
                    {
                    

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

