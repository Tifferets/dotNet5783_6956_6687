namespace DalApi;

 [Serializable]
public class doesNotExistException:Exception
{
    public doesNotExistException():base() { }
    override public string ToString() => "does not exist";
}
public class CantDeleteException : Exception
{
    public CantDeleteException() : base() { }
    override public string ToString() => "can't delete this product";
}
public class errorException : Exception
{
    public errorException() : base() { }
    override public string ToString() => "ERROR";
}
public class WrongIdException : Exception
{
    public WrongIdException() : base() { }
    override public string ToString() => "Wrong ID";
}


