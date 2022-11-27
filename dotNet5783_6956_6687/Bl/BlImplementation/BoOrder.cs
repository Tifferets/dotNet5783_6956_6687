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
                if(item.ShipDate <= DateTime.Now)// ?????????????
                {
                    //ofl.Status = BO.OrderStatus.shipped;
                }
                else if(item.ShipDate > DateTime.Now)
                {

                }
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
        if(orderId >= 100000 && orderId < 200000)//checks if id is correct
        {
            try
            {
                BO.Order order = new BO.Order();
                foreach(DO.Order item in dalList.order.GetAll())//loop to go over all the orders
                {
                    if(item.ID == orderId)//checking if the id is the same
                    {
                        order.ID = item.ID;
                        order.CustomerName = item.CustomerName;
                        order.CustomerAddress = item.CustomerAddress;   
                        order.CustomerEmail= item.CustomerEmail;
                        order.OrderDate = item.OrderDate;
                        order.ShipDate = item.ShipDate;
                        order.DeliveryDate=item.DeliveryDate;
                        List<BO.OrderItem> orderitems = new List<BO.OrderItem>();
                        double? totalprice = 0;
                        foreach(DO.OrderItem oitem in dalList.order.GetAllOrderItems())//getting a list of all the orderitems
                        {
                            BO.OrderItem oi= new BO.OrderItem(); 
                            oi.ID = oitem.OrderID;
                            oi.Price = oitem.Price;
                            oi.ProductID = oitem.ProductID;
                            oi.Amount= oitem.Amount;
                            oi.TotalPrice = oitem.Price * oitem.Amount;
                            orderitems.Add(oi);//adding to list
                            totalprice += oi.TotalPrice;
                        }
                        order.Items = orderitems;
                        order.TotalPrice=totalprice;
                    }
                }
                return order;
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
        DO.Order order= new DO.Order();
        foreach (DO.Order item in dalList.order.GetAll())
        {
            if(item.ID == orderId)
            {
                if(item.ShipDate < DateTime.Now )
                {
                   order = item;
                }
            }
        }
        order.ShipDate = DateTime.Now;//updates the DO order to 
        try
        {
            dalList.order.Update(order);//updating to the DO
        }
        catch
        {
            throw new BO.errorException();
        }
        BO.Order order1 = new BO.Order();
        foreach (DO.Order item in dalList.order.GetAll())//loop to go over all the orders
        {
            if (item.ID == orderId)//checking if the id is the same
            {
                order1.ID = item.ID;
                order1.CustomerName = item.CustomerName;
                order1.CustomerAddress = item.CustomerAddress;
                order1.CustomerEmail = item.CustomerEmail;
                order1.OrderDate = item.OrderDate;
                order1.ShipDate = DateTime.Now;
                order1.DeliveryDate = item.DeliveryDate;
                List<BO.OrderItem> orderitems = new List<BO.OrderItem>();
                double? totalprice = 0;
                foreach (DO.OrderItem oitem in dalList.order.GetAllOrderItems())//getting a list of all the orderitems
                {
                    BO.OrderItem oi = new BO.OrderItem();
                    oi.ID = oitem.OrderID;
                    oi.Price = oitem.Price;
                    oi.ProductID = oitem.ProductID;
                    oi.Amount = oitem.Amount;
                    oi.TotalPrice = oitem.Price * oitem.Amount;
                    orderitems.Add(oi);//adding to list
                    totalprice += oi.TotalPrice;
                }
                order1.Items = orderitems;
                order1.TotalPrice = totalprice;
                order1.Status = BO.OrderStatus.shipped;
            }
        }
        try
        {
            return order1;
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
    public Order UpdateDeliveryDate(int orderId)
    {

    }
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

