

namespace BO;

[Serializable]
public class doesNotExistException : Exception
{
    public doesNotExistException() : base() { }
    override public string ToString() => "does not exist";
}
public class errorException : Exception
{
    public errorException() : base() { }
    override public string ToString() => "ERROR";
}
public class WrongIDException : Exception
{
    public WrongIDException() : base() { }
    override public string ToString() => "ERROR";
}
