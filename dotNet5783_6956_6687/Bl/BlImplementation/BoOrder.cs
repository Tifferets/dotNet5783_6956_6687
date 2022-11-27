using BlApi;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation;

internal class BoOrder:IOrder
{
    private static DalApi.IDal dalList = new Dal.DalList();
    private BO.OrderStatus ordered;

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
                string status = OrderStatus(order);////make function
                double? price = 0;
                int amount = 0;
                foreach(DO.OrderItem oitem in dalList.order.GetAllOrderItems(item.ID))//loop to count the amount of products and total price
                {
                    amount++;
                    price += oitem.Price;
                }
                OrderForlist.Add(new BO.OrderForList
                {
                    ID = item.ID,
                    CustomerName = item.CustomerName,
                    AmountOfItems = amount,
                    TotalPrice = price,
                    Status = (BO.OrderStatus)Enum.Parse(typeof(BO.OrderStatus), status)//converting to enum
                });
                
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
    /// gets an order id, returns an order - for admin and user
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order GetOrderInfo(int orderId)
    {
        if(orderId >= 100000 && orderId < 200000)//checks if id is correct
        {
            try
            {
                DO.Order order = dalList.order.Get(orderId);
                double? totalprice = 0;
                int? oiamount = 0; 
                List<BO.OrderItem> orderitemList = new List<BO.OrderItem>();//list of orderitems
                IEnumerable<DO.OrderItem> oitms = dalList.order.GetAllOrderItems(orderId);//list od orderitems of this order
                foreach(DO.OrderItem item in oitms)//going over all the order items dor this order
                {
                    totalprice += item.Price* item.Amount;//the total price of the items
                    oiamount=item.Amount;//amount of items
                    DO.Product product= dalList.product.Get(item.ProductID);//the product
                    orderitemList.Add(new BO.OrderItem     //adding to the list
                    {
                        ID = item.OrderItemID,
                        ProductID = item.ProductID,
                        Name = product.Name,
                        Price = item.Price,
                        Amount = item.Amount,
                        TotalPrice = item.Price * item.Amount,

                    });
                }
                return new BO.Order
                {
                    ID = orderId,
                    CustomerName = order.CustomerName,
                    CustomerAddress = order.CustomerAddress,
                    CustomerEmail = order.CustomerEmail,
                    OrderDate = order.OrderDate,
                    ShipDate = order.ShipDate,
                    DeliveryDate = order.DeliveryDate,
                    Status = (BO.OrderStatus)Enum.Parse(typeof(BO.OrderStatus), OrderStatus(order)),//converting to enum
                    Items = orderitemList.ToList(),
                    TotalPrice = totalprice,
                };
            }
            catch
            {
                throw new BO.WrongIDException();
            }
        }
        throw new BO.WrongIDException();
    }
    /// <summary>
    /// updates the shipping date - for the admin
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order UpdateShippingDate(int orderId)
    {
        if (orderId < 100000)
        {
            throw new BO.WrongIDException();
        }
        try
        {
            DO.Order order = dalList.order.Get(orderId);//the order we want
            if (order.ShipDate != DateTime.MinValue)//never got changed
            {
                throw new BO.AlreadyShippedException();
            }
            order.ShipDate = DateTime.Now;
            dalList.order.Update(order);
            return GetOrderInfo(orderId);
        }
        catch
        {
            throw new BO.errorException();
        }
    }
    /// <summary>
    /// updates the delivery date - for the admin
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order UpdateDeliveryDate(int orderId)
    {
        if (orderId < 100000)
        {
            throw new BO.WrongIDException();
        }
        try
        {
            DO.Order order = dalList.order.Get(orderId);//the order we want
            if (order.ShipDate == DateTime.MinValue)//never got changed
            {
                throw new BO.NotShippedException();
            }
            if (order.DeliveryDate != DateTime.MinValue)//never got changed
            {
                throw new BO.AlreadyShippedException();
            }
            order.DeliveryDate = DateTime.Now;
            dalList.order.Update(order);
            return GetOrderInfo(orderId);
        }
        catch
        {
            throw new BO.errorException();
        }
    }
    /// <summary>
    /// the function gets an order id and returns the status of the order- for admin
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.OrderTracking OrderStatus(int orderId)
    {
        try
        {
            DO.Order order = dalList.order.Get(orderId);
            List<Tuple<BO.OrderStatus,DateTime>> list = new List<Tuple<BO.OrderStatus,DateTime>>();
            BO.OrderStatus status = BO.OrderStatus.ordered;
            if(order.OrderDate !=DateTime.MinValue)
            {
                list.Add(Tuple.Create(BO.OrderStatus.ordered,order.OrderDate));
            }
            if (order.ShipDate != DateTime.MinValue)
            {
                list.Add(Tuple.Create(BO.OrderStatus.shipped, order.ShipDate));
                status=BO.OrderStatus.shipped;
            }
            if (order.DeliveryDate != DateTime.MinValue)
            {
                list.Add(Tuple.Create(BO.OrderStatus.delivered, order.DeliveryDate));
                status=BO.OrderStatus.delivered;
            }
            BO.OrderTracking orderTracking= new BO.OrderTracking() { ID = orderId, Status= status , tracking=list};
            return orderTracking;
        }
        catch
        {
            throw new BO.errorException();
        }

    }
    /// <summary>
    /// BONUS!!
    /// </summary>
    /// <param name="order"></param>
    public void UpdateOrder(Order order);

}

