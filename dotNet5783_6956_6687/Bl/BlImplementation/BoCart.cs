using BlApi;
using BO;
using DalApi;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation;

internal class BoCart : ICart
{
    private static DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// adds a product to the cart. if product is alredy in cart adds another to the amount and price, if not adds it
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Cart AddProductToCart(Cart cart, int productId)
    {
        try
        {
            if (productId < 300000 || productId > 499999)//checking product id
                throw new WrongIDException();
            if (cart.Items == null)//the cart is empty
            {
                List<OrderItem> items = new List<OrderItem>();
                DO.Product? product = dal?.product.GetAll().FirstOrDefault(x => x?.ID == productId && x?.InStock > 0);//checks if the product exist at all
                if (product != null)
                {
                    BO.OrderItem oi = new BO.OrderItem()
                    {
                        Price = (double)product?.Price,
                        ProductID = (int)product?.ID,
                        Name = product?.Name,
                        Amount = 1,
                        TotalPrice = (double)product?.Price,

                    };
                    items.Add(oi);
                    cart.Items= items;
                    cart.TotalPrice += (double)product?.Price;//added 1 product to the cart
                }
            }
            else
            {
                BO.OrderItem? orderItem = cart.Items.ToList().FirstOrDefault(x => x.ProductID == productId);//orderItem is null if the product doesnt exist in the cart
                if (orderItem != null) //if the product already exists in the cart
                {
                    DO.Product? product = dal?.product.GetAll().FirstOrDefault(x => x?.ID == productId && x?.InStock > 0);//checks if the product exist at all
                    if (product != null)
                    {
                        cart.Items.Remove(orderItem);//removes the orderitem from the cart
                        orderItem.Amount += 1;
                        orderItem.TotalPrice = orderItem.TotalPrice + (double)product?.Price;
                        cart.TotalPrice = cart.TotalPrice + (double)product?.Price;
                        cart.Items.Add(orderItem);//adds the updated order item
                    }
                    else
                        throw new BO.NoMoreInStockException();
                }
                else//the product doesnt exist yet in the cart
                {
                    DO.Product? product = dal?.product.GetAll().FirstOrDefault(x => x?.ID == productId && x?.InStock > 0);//checks if the product exist at all
                    if (product != null)
                    {
                        BO.OrderItem oi = new BO.OrderItem()
                        {
                            Price = (double)product?.Price,
                            ProductID = (int)product?.ID,
                            Name = product?.Name,
                            Amount = 1,
                            TotalPrice = (double)product?.Price,
                        };
                        cart.Items.Add(oi);
                        cart.TotalPrice += (double)product?.Price;//added 1 product to the cart
                    }
                    else throw new BO.CouldntFindProductException();
                }
            }
        }
        catch (Exception ex) { throw new CouldntAddProductException(); }//if nothing worked
   return cart;
 }
        //    {
        //        BO.Product p = dal.product.GetAll().FirstOrDefault(x => x.Value.ID == productId);//p is the product that we want to add
        //        if (p == null || p.InStock < orderItem.Amount) //if the product doesnt exist or there is not enough left
        //            throw new BO.NoMoreInStockException();
        //        else
        //        {
        //            orderItem.Amount = orderItem.Amount + 1;//updates the amount
        //            orderItem.TotalPrice = orderItem.Price * orderItem.Amount;//updates the price
        //            cart.TotalPrice = cart.TotalPrice + orderItem.Price;//updates the final price of the cart
        //            List<BO.OrderItem>? temp = cart.Items.Where(x => x.ProductID != productId).ToList();//take all the products but the product that we updated
        //            temp.Add(orderItem);//addes orderitem to the cart
        //                                // cart.Items = cart.Items.Add(orderItem);//addes orderitem to the cart
        //        }

        //}
        //else//if the product doesnt exist
        //{

        //}

        //bool flag = false;
        //if (productId < 300000 || productId > 499999)//checking product id
        //    throw new WrongIDException();
        //foreach (BO.OrderItem? item in (cart.Items ?? throw new BO.NullException()))//goes through all the items in the cart
        //{
        //    if (item?.ProductID == productId)//checking if the product already exists in the cart
        //    {
        //        flag = true;


        //        foreach (DO.Product? item1 in (dal?.product.GetAll() ?? throw new BO.NullException()))//goes through all the products that exist in general
        //        {
        //            if (item1?.ID == productId && item1?.InStock > 0)//if it exists and has enough in stock
        //            {
        //                item.Amount = item.Amount + 1;//addes 1 more of the product to the cart
        //                item.TotalPrice = item.TotalPrice + item.Price;
        //                cart.TotalPrice = item.TotalPrice;
        //                break;
        //            }
        //            else
        //                throw new NoMoreInStockException();
        //        }
        //        break;
        //    }
        //}
        //if (flag == false)//if the product does not exists in the cart
        //{
        //    foreach (DO.Product item in (dal?.product.GetAll() ?? throw new BO.NullException()))//goes through all the products that exist in general
        //    {
        //        if (item.ID == productId)//if it exists and has aenough in stock
        //        {
        //            if (item.InStock > 0)
        //            {
        //                double price = item.Price;
        //                cart.TotalPrice = price + cart.TotalPrice;
        //                //BO.OrderItem newItem = new BO.OrderItem()//creats a new orderitem
        //                //{
        //                //    ProductID = productId,
        //                //    ID = item.ID,
        //                //    Name = item.Name,
        //                //    Price = item.Price,
        //                //    TotalPrice = item.Price,
        //                //    Amount = 1
        //                //};
        //                //cart.TotalPrice = newItem.TotalPrice + cart.TotalPrice;
        //            }
        //            else
        //                throw new NoMoreInStockException();//if the product already exists in the cart but the amount is 0
        //        }
        //    }
        //}

   
    /// <summary>
    /// update an amount of a product in the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <param name="newAmount"></param>
    public Cart UpdateAmountOfProductInCart(Cart? cart, int productId, int newAmount)
    {
        try
        {
            if (cart?.Items == null)
                throw new NoItemsInCartException();

            if (productId < 300000 || productId > 490000)
                throw new BO.WrongIDException();
            //if (cart.CustomerAddress == null || cart.CustomerAddress == " " || CheckEmail(cart.CustomerAddress) || cart.CustomerName == null || cart.CustomerName == " ")
            //{
            //    throw new BO.MissingCustomersInfoException();
            //}
            int dif = 0; //the difference between the new amount and the old amount
            List<BO.OrderItem?> lst = (from OrderItem? item in cart.Items
                                       select item).ToList();//copies all the orderItems in item in cart to the lst
            BO.OrderItem? theProductToUpdate = cart.Items.FirstOrDefault(x=> x.ProductID == productId);//finds the product to updates its amount
            if(theProductToUpdate != null)//if the product were updating exists
            {

                if (newAmount == 0)
                {
                    lst.Remove(theProductToUpdate);//removes the orderitem from lst
                    cart.Items = lst;//updates the cart
                    cart.TotalPrice = cart.TotalPrice - theProductToUpdate.TotalPrice;
                }
                else if (newAmount > theProductToUpdate.Amount)//wanted more
                {
                    dif = newAmount - theProductToUpdate.Amount;
                    cart.TotalPrice = cart.TotalPrice + dif * theProductToUpdate.Price;
                    theProductToUpdate.Amount = newAmount;
                    theProductToUpdate.TotalPrice = newAmount * theProductToUpdate.Price;
                }
                else if (newAmount < theProductToUpdate.Amount)//wanted less
                {
                    dif = theProductToUpdate.Amount- newAmount;
                    cart.TotalPrice = cart.TotalPrice - dif * theProductToUpdate.Price;
                    theProductToUpdate.Amount = newAmount;
                    theProductToUpdate.TotalPrice = newAmount * theProductToUpdate.Price;
                }
            }
            else
            {
                throw new BO.CouldntFindProductException();
            }

            return cart;
            //foreach (BO.OrderItem? item in cart.Items)
            //{
            //    if (item?.ProductID == productId)
            //    {
            //        if (newAmount == 0)
            //        {
            //            lst.Remove(item);//removes the orderitem from lst
            //            cart.Items = lst;//updates the cart
            //            break;
            //        }
            //        if (newAmount > item.Amount)//wanted more
            //        {
            //            dif = newAmount - item.Amount;
            //            cart.TotalPrice = cart.TotalPrice + dif * item.Price;
            //            item.Amount = newAmount;
            //            break;
            //        }
            //        if (newAmount < item.Amount)//wanted less
            //        {
            //            dif = newAmount - item.Amount;
            //            cart.TotalPrice = cart.TotalPrice - dif * item.Price;
            //            item.Amount = newAmount;
            //            break;
            //        }
            //    }
            //}

        }
        catch(Exception)
        {
            throw new BO.CantUpDateException();
        }
    }
    /// <summary>
    /// the function confirms the cart get, gets the cart and the customers info
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="name"></param>
    ///<param name="address"></param>
    /// <param name="email"></param>
    public void confirmCart(Cart cart)//, string name, string address, string email)
    {
        //if(cart.CustomerName == null || cart.CustomerAddress ==null||    !cart.CustomerEmail.Contains('@')
        //    throw new MissingCustomersInfoException();
        //foreach (OrderItem item in cart.Items)
        //{
        //    DO.Product? product = dal?.product.GetAll().FirstOrDefault(x => x.Value.ID == item.ProductID);
        //    if (product == null)
        //        throw new BO.CouldntFindProductException();
        //    else
        //    {
        //        if(product?.InStock < item.Amount)
        //            throw new BO.NoMoreInStockException();
        //        if(item.Amount <=0 )
        //            throw new BO.errorException();

        //    }
        //}
        ////if alls good

        //DO.Order order = new DO.Order()
        //{
        //    DeliveryDate =null,
        //    ShipDate=null,
            
        //    CustomerAddress = cart.CustomerAddress,
        //    CustomerEmail = cart.CustomerEmail,
        //    CustomerName = cart.CustomerName,
        //    OrderDate = DateTime.Now,
        //};
        //try
        //{

        //    order.ID = dal?.order.Add(order) ?? 0;

        //}
        //catch (Exception ex) { throw new BO.errorException(); }
        //foreach (OrderItem item in cart.Items)
        //{
        //    DO.OrderItem orderItem = new DO.OrderItem()
        //    {
        //        Amount = item.Amount,
        //        Price = item.Price,
        //        ProductID = item.ProductID,
        //        OrderID = order.ID,
        //        OrderItemID = item.ID
        //    };

        //    orderItem.OrderItemID = (int)dal?.orderItem.Add(orderItem);
        //}
        















        bool checkIfWorked = true;
        try
        {
           // if (name == null || address == null || CheckEmail(email) == false)//makes sure all the customers info is correct
              //  throw new MissingCustomersInfoException();
            if (cart.Items == null)
                throw new BO.NoItemsInCartException();

            bool flag = false;

            foreach (OrderItem? item in cart.Items ?? throw new BO.NullException())
            {
                if (item?.Amount < 0)
                    throw new WrongAmountException();

               // DO.Product product = dal?.product.GetAll().FirstOrDefault(x => x.ID == item.ID);
                foreach (DO.Product? item1 in (dal?.product.GetAll() ?? throw new BO.NullException()))//goes through all the product looking for the product in the cart
                {
                    if (item1?.ID == item?.ProductID)
                    {
                        if (item1?.InStock < item?.Amount)//if the product exists but its not in stock
                            throw new NoMoreInStockException();
                        else
                            flag = true;
                        break;//move on to look for the next product
                    }
                }
                if (flag == false)//if we didnt find the product in general then throw
                    throw new NoMoreInStockException();
            }
            if (flag == true)
            {
                DO.Order order = new DO.Order()
                {
                    CustomerAddress = cart.CustomerAddress,
                    CustomerEmail = cart.CustomerEmail,
                    CustomerName = cart.CustomerName,
                    OrderDate = DateTime.Now,
                };
                int id;
                try
                {
                    order.ID = dal?.order.Add(order) ?? 0;
                }
                catch (Exception )
                {
                    throw new BO.errorException();
                }
                foreach (OrderItem? item in cart.Items)//goes through all the orderitems in cart
                {
                    DO.OrderItem orderItem = new DO.OrderItem()
                    {
                        Amount = item.Amount,
                        Price = item.Price,
                    };
                    orderItem.OrderItemID = (int)dal?.orderItem.Add(orderItem);
                    orderItem.OrderID = order.ID;
                    orderItem.ProductID = item.ProductID;
                    dal?.orderItem.Update(orderItem);
                }
                foreach (OrderItem? item in cart.Items)//goes through all the orderitems in cart
                {
                    //DO.OrderItem orderItem = new DO.OrderItem()//creats a new order item
                    //{
                    //    Amount = item.Amount,
                    //    OrderID = id,
                    //    OrderItemID = item.ID,
                    //    Price = item.Price,
                    //    ProductID = item.ProductID,
                    //};

                    // foreach (DO.Product p in dal?.product.GetAll()?? throw new BO.NullException())//goes through all the products in do
                    //   {
                    DO.Product? p = dal?.product.GetAll().ToList().FirstOrDefault(x => x.Value.ID == item?.ProductID);
                    
                        if(p != null)
                        {

                        DO.Product product = new DO.Product()
                        {
                            ID = p.Value.ID,
                            Price = p.Value.Price,
                            Name = p.Value.Name,
                            Category = p.Value.Category,
                            InStock = p.Value.InStock - item.Amount,
                        };
                        dal?.product.Update(product);
                        }
                        //if (p.ID == item?.ProductID)//if its the product then change the amount in stock
                        //{
                        //    DO.Product product = new DO.Product()
                        //    {
                        //        ID = p.ID,
                        //        Price = p.Price,
                        //        Name = p.Name,
                        //        Category = p.Category,
                        //        InStock = p.InStock - item.Amount,
                        //    };
                        //    dal?.product.Update(p);
                        //    break;
                        //}
                 //   }
                }
            }
    }

        catch (Exception e)
        {
            Console.WriteLine(e);
            checkIfWorked = false;
        }
        if (checkIfWorked == true)
            Console.WriteLine("cart confirmed");
    }
    private bool CheckEmail(string email)//returns true if the email is proper else returns false
    {
        if(email == null) return false;
        if (email.Contains('@')) return true;
        return false;
    }
}
