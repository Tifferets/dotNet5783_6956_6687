namespace DalApi;

 [Serializable]

public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

public class doesNotExistException : Exception
{
    public override string Message => "Item does not exist";
    public override string ToString()
    {
        return Message;
    }
}
public class NullException : Exception
{
    public override string Message => "Item was null";
    public override string ToString()
    {
        return Message;
    }
}
public class CantDeleteException : Exception
{
    public override string Message => "Sorry, can't delete this product";
    public override string ToString()
    {
        return Message;
    }
}

public class errorException : Exception
{
    public override string Message => "ERROR";
    public override string ToString()
    {
        return Message;
    }
}

public class WrongIdException : Exception
{
    public override string Message => "ID format not correct";
    public override string ToString()
    {
        return Message;
    }
}
public class NoNameException : Exception
{
    public override string Message => "Name was not entered";
    public override string ToString()
    {
        return Message;
    }
}
public class EmptyListException : Exception
{//used in xml tools
    public override string Message => "Empty is List";
    public override string ToString()
    {
        return Message;
    }
}
public class CantGetListFromXMLException : Exception
{//used in xml tools
    public override string Message => "Cant Get List From XML";
    public override string ToString()
    {
        return Message;
    }
}
public class CouldntAddException : Exception
{//used in xml tools
    public override string Message => "Couldn't add product";
    public override string ToString()
    {
        return Message;
    }
}



