

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
    override public string ToString() => "already not exist";
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
public class WrongDataException : Exception
{
    public WrongDataException() : base() { }
    override public string ToString() => "Data not right";
}
