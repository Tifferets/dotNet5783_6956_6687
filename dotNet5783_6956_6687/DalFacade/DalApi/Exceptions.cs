
namespace DalApi;

public class doesNotExist:Exception
{
    doesNotExist( Exception ex) { Console.WriteLine("does Not Exist"); }
}
public class error : Exception
{
    error(Exception ex) { Console.WriteLine("ERROR"); }
}
