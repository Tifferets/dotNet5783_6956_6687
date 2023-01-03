using BlApi;
using BO;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation;

internal class BoOrder: IOrder
{
    private static DalApi.IDal? dal = DalApi.Factory.Get();
    //private BO.OrderStatus ordered;

    /// <summary>
    /// returns the list of orders- for the admin
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> GetOrderList()
    {
       //List<BO.OrderForList> OrderForlist = new List<BO.OrderForList>();
        //  list of orderForList
        try
        {
            var neww = (from DO.Order item in dal?.order.GetAll() ?? throw new BO.NullException()
                        let amountOfitems = dal?.order.GetAllOrderItems(item.ID).Count()
                        let totalPrice = dal?.order.GetAllOrderItems(item.ID).Sum(x => x?.Price)
                        let status = (BO.OrderStatus)OrderStatus(item.ID).Status
                        let oilist = dal?.order?.GetAllOrderItems(item.ID).OrderByDescending(x => x?.OrderItemID)
                        select new BO.OrderForList
                        {
                            ID = item.ID,
                            CustomerName = item.CustomerName,
                            Status = status,
                            AmountOfItems = amountOfitems ?? 0,
                            TotalPrice = totalPrice ?? 0,
                        }).ToList();

            return neww;
        
            //foreach (DO.Order item in dal?.order.GetAll() ?? throw new BO.NullException())
            //{
            //    BO.OrderTracking orderTracking = OrderStatus(item.ID);
            //    string? statusee = status(orderTracking);
            //    BO.OrderStatus stauss = (BO.OrderStatus)Enum.Parse(typeof(BO.OrderStatus), statusee);//converting to enum type
            //    double price = 0;
            //    int amount = 0;
            //    foreach (DO.OrderItem oitem in (dal?.order.GetAllOrderItems(item.ID) ?? throw new BO.NullException()))//loop to count the amount of products and total price
            //    {
            //        amount++;
            //        price += oitem.Price;
            //    }
            //    OrderForlist.Add(new BO.OrderForList
            //    {
            //        ID = item.ID,
            //        CustomerName = item.CustomerName,
            //        AmountOfItems = amount,
            //        TotalPrice = price,
            //        Status = stauss,//converting to enum
            //    });
            //}
            //IEnumerable<BO.OrderForList> orderForLists = OrderForlist;//list to return
            //return orderForLists;
        }
        catch (Exception)
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
                DO.Order? order = dal?.order.GetSingle(x => x?.ID == orderId);
                List<BO.OrderItem> orderitemList = new List<BO.OrderItem>();//list of orderitems
                double totalprice = 0;
                var orderitems = from DO.OrderItem item in dal?.order.GetAllOrderItems(orderId) ?? throw new BO.NullException()
                                     //let totalPrice = dal?.order.GetAllOrderItems(item.OrderID).Sum(x => x?.ProductID)
                                     // let amountOfitems = dal?.order.GetAllOrderItems(item.OrderID).Count()
                                 let product = dal?.product.GetSingle(x => x?.ID == item.ProductID)//the product by id
                                 select new BO.OrderItem     //adding to the list
                                 {
                                     ID = item.OrderItemID,
                                     ProductID = item.ProductID,
                                     Name = product?.Name,
                                     Price = item.Price,
                                     Amount = item.Amount,
                                     TotalPrice = item.Amount * item.Price,
                                 };
                //foreach (DO.OrderItem item in dal?.order.GetAllOrderItems(orderId) ?? throw new BO.NullException())//going over all the order items dor this order
                //{
                //    // totalprice += item.Price* item.Amount;//the total price of the items
                //    // oiamount = item.Amount;//amount of items
                //    DO.Product? product = dal?.product.GetSingle(x => x?.ID == item.ProductID);//the product by id 
                //    orderitemList.Add(new BO.OrderItem     //adding to the list
                //    {
                //        ID = item.OrderItemID,
                //        ProductID = item.ProductID,
                //        Name = product?.Name,
                //        Price = item.Price,
                //        Amount = item.Amount,
                //        TotalPrice = item.Price * item.Amount,

                //    });
                //}
                foreach (BO.OrderItem item in orderitems)//adding all the items to the list
                {
                    orderitemList.Add(item);
                    totalprice += item.TotalPrice;
                }
                BO.OrderTracking orderTracking = OrderStatus(orderId);
                string statusee = status(orderTracking)!;
                BO.OrderStatus stauss = (BO.OrderStatus)Enum.Parse(typeof(BO.OrderStatus), statusee);//converting to enum type
                return new BO.Order
                {
                    ID = orderId,
                    CustomerName = order?.CustomerName,
                    CustomerAddress = order?.CustomerAddress,
                    CustomerEmail = order?.CustomerEmail,
                    OrderDate = order?.OrderDate,
                    ShipDate = order?.ShipDate,
                    DeliveryDate = order?.DeliveryDate,
                    Status = (BO.OrderStatus)System.Enum.Parse(typeof(BO.OrderStatus), stauss.ToString()),//converting to enum
                    Items = orderitemList,
                    TotalPrice = (double)totalprice,
                };
            }
            catch
            {
                throw new BO.WrongIDException();
            }
        }
        throw new BO.WrongIDException();

        //foreach (DO.OrderItem item in dal?.order.GetAllOrderItems(orderId) ?? throw new BO.NullException())//going over all the order items dor this order
        //{
        //   // totalprice += item.Price* item.Amount;//the total price of the items
        //   // oiamount = item.Amount;//amount of items
        //    DO.Product? product = dal?.product.GetSingle(x=> x?.ID == item.ProductID);//the product by id 
        //    orderitemList.Add(new BO.OrderItem     //adding to the list
        //    {
        //        ID = item.OrderItemID,
        //        ProductID = item.ProductID,
        //        Name = product?.Name,
        //        Price = item.Price,
        //        Amount = item.Amount,
        //        TotalPrice = item.Price * item.Amount,

        //    });
        //}
    }
    /// <summary>
    /// updates the shipping date - for the admin
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public BO.Order UpdateShippingDate(int orderId)
    {
        if (orderId < 100000 || orderId >= 200000)
        {
            throw new BO.WrongIDException();
        }
        try
        {
            DO.Order order = (DO.Order)dal?.order.GetSingle(x => x?.ID == orderId);//the order we want
            if (order.ShipDate != null)//never got changed
            {
                throw new BO.AlreadyShippedException();
            }
            order.ShipDate = DateTime.Now;// DateTime.Now;
            dal?.order.Update(order);
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
            DO.Order order = (DO.Order)dal?.order.GetSingle(x => x?.ID == orderId);//the order we want
            if (order.ShipDate == null)//never got changed
            {
                throw new BO.NotShippedException();
            }
            if (order.DeliveryDate != null)//never got changed
            {
                throw new BO.AlreadyShippedException();
            }
            order.DeliveryDate = DateTime.Now;
            dal?.order.Update(order);
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
            DO.Order? order = (DO.Order?)dal?.order?.GetSingle(x => x?.ID == orderId);
            if (order == null)
            {
                throw new BO.WrongIDException();
            }
            List<Tuple<BO.OrderStatus,DateTime>> list = new List<Tuple<BO.OrderStatus,DateTime>>();
            BO.OrderStatus status = BO.OrderStatus.ordered;
            if (order?.OrderDate != null && order?.OrderDate != DateTime.MinValue)
            {
                list.Add(Tuple.Create(BO.OrderStatus.ordered, (DateTime)order?.OrderDate));
            }
            if (order?.ShipDate != null && order?.ShipDate != DateTime.MinValue)
            {
                list.Add(Tuple.Create(BO.OrderStatus.shipped, (DateTime)order?.ShipDate));
                status = BO.OrderStatus.shipped;
            }
            if (order?.DeliveryDate != null && order?.DeliveryDate != DateTime.MinValue)
            {
                list.Add(Tuple.Create(BO.OrderStatus.delivered, (DateTime)order?.DeliveryDate));
                status = BO.OrderStatus.delivered;
            }
            BO.OrderTracking orderTracking = new BO.OrderTracking() { ID = orderId, Status = status, tracking = list };
            return orderTracking;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
    /// <summary>
    /// returns a string from a order status 
    /// </summary>
    /// <param name="orderTracking"></param>
    /// <returns></returns>
    private string? status(BO.OrderTracking orderTracking)=> orderTracking.Status.ToString();
  
    /// <summary>
    /// BONUS!!
    /// </summary>
    /// <param name="order"></param>
   //public BO.Order UpdateOrder(BO.Order order, int orderitemId,int newAmount)
   // {
   //     try
   //     {
   //        List<BO.OrderItem?> lst = new List<BO.OrderItem?>();
   //         foreach (BO.OrderItem item in order.Items)
   //         {
   //             lst.Add(item);//list of all
   //         }
   //         int dif = 0; //the difference between the new amount
   //         foreach (BO.OrderItem? item in order.Items)
   //         {
   //             if(item?.ID == orderitemId)
   //             { 
   //             if (newAmount == 0)
   //                 {
   //                     lst.Remove(item);//removes the orderitem from lst
   //                     order.Items = lst;//updates the cart
   //                     break;
   //                 }
   //                 if (newAmount > item.Amount)//wanted more
   //                 {
   //                   //  DO.Product pro= dalList.product.GetSingle(x=> x?.))
   //                     dif = newAmount - item.Amount;
   //                     order.TotalPrice = order.TotalPrice + dif * item.Price;
   //                     item.Amount = newAmount;
   //                     break;
   //                 }
   //                 if (newAmount < item.Amount)//wanted less
   //                 {
   //                     dif = newAmount - item.Amount;
   //                     order.TotalPrice = order.TotalPrice - dif * item.Price;
   //                     item.Amount = newAmount;
   //                     break;
   //                 }
   //             }
   //         }
   //         return order;
   //     }
        
   //     catch(Exception ex)
   //     {

   //     }
   // }

}

