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
public class NoMoreInStockException : Exception
{//used in cart
    public NoMoreInStockException() : base() { }
    override public string ToString() => "No more in stock or doesn't exist in stock";
}
