using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

public class DoOrder:IOrder
{
    static string fpath = @"Order";
    public static void SaveListToXML(List<Order> listOfOrders, string path)
    {
        FileStream fs = new FileStream(path, FileMode.Create);
        XmlSerializer x =new XmlSerializer(listOfOrders.GetType());
        x.Serialize(fs, listOfOrders);
    }
    public static List<Order> LoadListFromXML(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        XmlSerializer x = new XmlSerializer(typeof(List<Order>));
        return (List<Order>)x.Deserialize(fs);
    }
}
