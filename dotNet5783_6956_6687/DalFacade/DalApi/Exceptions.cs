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
public class CouldntAddProductException : Exception
{//used in xml tools
    public override string Message => "Couldn't add product";
    public override string ToString()
    {
        return Message;
    }
}
public class CantSaveListToXMLException : Exception
{//used in xml tools
    public override string Message => "Cant Save List To XML";
    public override string ToString()
    {
        return Message;
    }
}
public class CantLoadException : Exception
{//used in xml tools
    public override string Message => "Cant Load";
    public override string ToString()
    {
        return Message;
    }
}
public class FileUploadProductNotSuccessfulException : Exception
{//used in doproduct
    public override string Message => "file upload product not successful";
    public override string ToString()
    {
        return Message;
    }
}
public class CantGetSingleException : Exception
{//used in doproduct and in doOrderitem
    public override string Message => "Cant Get a Single one";
    public override string ToString()
    {
        return Message;
    }
}
public class CantGetListOfAllProducts : Exception
{//used in doproduct
    public override string Message => "Can not get list of all products";
    public override string ToString()
    {
        return Message;
    }
}
public class CantAddProductException : Exception
{//used in doproduct
    public override string Message => "Cant Add Product";
    public override string ToString()
    {
        return Message;
    }
}
public class CantDeleteProductException : Exception
{//used in doproduct
    public override string Message => "Cant Delete Product";
    public override string ToString()
    {
        return Message;
    }
}
public class CantUpdateProductException : Exception
{//used in doproduct
    public override string Message => "Cant Update Product";
    public override string ToString()
    {
        return Message;
    }
}
public class CouldntAddOrderItemException : Exception
{//used in doOrderItem
    public override string Message => "Couldnt Add OrderItem";
    public override string ToString()
    {
        return Message;
    }
}
public class CantUpdateOrderItemException : Exception
{//used in doOrderItem
    public override string Message => "Cant Update OrderItem";
    public override string ToString()
    {
        return Message;
    }
}
public class CouldntAddOrderException : Exception
{//used in doOrder
    public override string Message => "Couldnt Add Order Exception";
    public override string ToString()
    {
        return Message;
    }
}

public class CouldntUpdateOrderException : Exception
{//used in doOrder
    public override string Message => "Couldnt Update Order";
    public override string ToString()
    {
        return Message;
    }
}






