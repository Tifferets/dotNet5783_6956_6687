using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class doesNotExist:Exception
    {
        doesNotExist( Exception ex) { Console.WriteLine("does Not Exist"); }
    }
}
