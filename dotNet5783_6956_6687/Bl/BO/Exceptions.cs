

namespace BO;

[Serializable]
public class doesNotExistException : Exception
{
    public doesNotExistException(string ms) : base(ms) { }
    override public string ToString() => "does not exist";
}
public class errorException : Exception
{
    public errorException(string ms) : base(ms) { }
    override public string ToString() => "ERROR";
}
