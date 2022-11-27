namespace BO;

[Serializable]

public class doesNotExistException : Exception
{
    public doesNotExistException() : base() { }
    override public string ToString() => "does not exist";
}
public class alreadyExistException : Exception
{
    public alreadyExistException() : base() { }
    override public string ToString() => "already exists";
}
public class errorException : Exception
{
    public errorException() : base() { }
    override public string ToString() => "ERROR";
}
public class WrongIDException : Exception
{
    public WrongIDException() : base() { }
    override public string ToString() => "Wrong ID";
}
public class WrongDataException : Exception
{
    public WrongDataException() : base() { }
    override public string ToString() => "Data not right";
}
public class CantDeleteException : Exception
{
    public CantDeleteException() : base() { }
    override public string ToString() => "can't delete this product";
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
