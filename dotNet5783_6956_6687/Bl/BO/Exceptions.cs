using System.Runtime.InteropServices;

namespace BO;

[Serializable]

public class NullException : Exception
{
    public override string Message => "Item was null";
    public override string ToString()
    {
        return Message;
    }
}
public class doesNotExistException : Exception
{
    public override string Message => "does not exist";
    public override string ToString()
    {
        return Message;
    }
}
public class InStockException : Exception
{
    public override string Message => "No amount in stock";
    public override string ToString()
    {
        return Message;
    }
}

    public class PriceNotGoodException : Exception
{
    public override string Message => "Price cannot be zero or negative ";
    public override string ToString()
    {
        return Message;
    }
}
public class alreadyExistException : Exception
{
    public override string Message => "This product already exists";
    public override string ToString()
    {
        return Message;
    }
}
public class errorException : Exception
{
    public override string Message => "ERROR";
    public override  string ToString()
    {
        return Message;
    }
}
public class WrongIDException : Exception
{
    public override string Message => "Wrong ID entered";
    public override string ToString()
    {
        return Message;
    }
}

public class WrongDataException : Exception
{
    public override string Message => "Data entered is not right";
    public override string ToString()
    {
        return Message;
    }
}
public class CantDeleteException : Exception
{
    public override string Message => "can't delete this product";
    public override string ToString()
    {
        return Message;
    }
}

public class CantUpDateException : Exception
{
    public override string Message => "can't update this product";
    public override string ToString()
    {
        return Message;
    }
}

public class CantGetException : Exception
{
    public override string Message => "can't get this product";
    public override string ToString()
    {
        return Message;
    }
}

public class NoMoreInStockException : Exception
{
    public override string Message => "No more in stock";
    public override string ToString()
    {
        return Message;
    }
}
public class AlreadyShippedException : Exception
{
    public override string Message => "The order has already been shipped";
    public override string ToString()
    {
        return Message;
    }
}

public class AlreadyDeliverdException : Exception
{
    public override string Message => "The order has already been deliverd";
    public override string ToString()
    {
        return Message;
    }
}

public class NotShippedException : Exception
{
    public override string Message => "The order wasn't shipped yet";
    public override string ToString()
    {
        return Message;
    }
}
public class MissingCustomersInfoException : Exception
{
    public override string Message => "Missing customers Information";
    public override string ToString()
    {
        return Message;
    }
}
public class WrongAmountException : Exception
{
    public override string Message => "Wrong Amount";
    public override string ToString()
    {
        return Message;
    }
}
public class NoItemsInCartException : Exception
{ 
    public override string Message => "No items in cart";
    public override string ToString()
    {
        return Message;
    }
}
