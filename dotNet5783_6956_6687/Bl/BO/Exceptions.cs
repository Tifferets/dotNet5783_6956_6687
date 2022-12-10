namespace BO;

[Serializable]


public class doesNotExistException : Exception
{
    public override string Message => "does not exist";
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
    public CantUpDateException() : base() { }
    override public string ToString() => "can't update this product";
}
public class CantGetException : Exception
{
    public CantGetException() : base() { }
    override public string ToString() => "can't get this product";
}
public class NoMoreInStockException : Exception
{//used in bocart
    public NoMoreInStockException() : base() { }
    override public string ToString() => "No more in stock or doesn't exist in stock";
}
public class AlreadyShippedException : Exception
{
    public AlreadyShippedException() : base() { }
    override public string ToString() => "The order has already been shipped ";
}
public class AlreadyDeliverdException : Exception
{
    public AlreadyDeliverdException() : base() { }
    override public string ToString() => "The order has already been deliverd ";
}
public class NotShippedException : Exception
{
    public NotShippedException() : base() { }
    override public string ToString() => "The order wasn't shipped yet";
}
public class MissingCustomersInfoException : Exception
{//used in bocart
    public MissingCustomersInfoException() : base() { }
    override public string ToString() => "Missing customers Information";
}
public class WrongAmountException : Exception
{//used in bocart
    public WrongAmountException() : base() { }
    override public string ToString() => "Wrong Amount";
}
public class NoItemsInCartException : Exception
{//used in bocart
    public NoItemsInCartException() : base() { }
    public override string ToString() => "No items in cart";
}
