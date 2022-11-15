using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

 [Serializable]
public class doesNotExistException:Exception
{
    public doesNotExistException(string ms):base(ms) { }
    override public string ToString() => "does not exist";
}
public class errorException : Exception
{
    public errorException(string ms) : base(ms) { }
    override public string ToString() => "ERROR";
}

