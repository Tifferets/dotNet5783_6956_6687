using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed internal class DalXml:IDal
{
    public IProduct DoProduct { get; } = new Dal.DoProduct();
    public IOrder DoOrder { get; } = new Dal.DoOrder();
    public IOrderItem DoOrderItem { get; } = new Dal.DoOrderItem();
}
