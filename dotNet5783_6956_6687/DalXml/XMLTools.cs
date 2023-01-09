using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

public class XMLTools
{
    static string dir = @"..\xml\";
    static XMLTools()
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }
    //  static string fpath = @"Order";
    public static void SaveListToXML<T>(List<T> listOfOrders, string path)
    {
        try
        {
            FileStream fs = new FileStream(dir + path, FileMode.Create);
            XmlSerializer x = new XmlSerializer(listOfOrders.GetType());
            x.Serialize(fs, listOfOrders);
            fs.Close();
        }
        catch (Exception ex)
        {
            throw new("Cant Save List To XML Exception");
        }
    }
    public static List<T> LoadListFromXML<T>(string path)
    {
        try
        {
            if (File.Exists(dir + path))
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream fs = new FileStream(dir + path, FileMode.Open);
                list = (List<T>)x.Deserialize(fs);
                fs.Close();
                return list;
            }
            else
                return new List<T>();
        }
        catch (Exception ex)
        {
            throw new DalApi.CantGetListFromXMLException();
        }
    }
}
public static class Config
{
    private static XElement configRoot;
    private static string configPath = @"..\xml\Config.xml";
    public static void LoadData()
    {
        try
        {
            configRoot = XElement.Load(configPath);
        }
        catch (Exception ex)
        {
            throw new Exception("cant load");
        }
    }
    static Config()
    {
        if (File.Exists(configPath) == false)
        { 
            configRoot = new XElement("Config", new XElement("OrderID", 100000), new XElement("OrderItemID", 200000));
            configRoot.Save(configPath);
        }
        else
            LoadData();
    }
    public static int getNewOrderID()
    {
        LoadData();
        XElement OrderId = configRoot.Element("OrderID");
        OrderId.Value = (Convert.ToInt32(OrderId.Value) + 1).ToString();
        configRoot.Save(configPath);
        return (Convert.ToInt32(OrderId.Value));
    }
    public static int getNewOrderItemID()
    {
        LoadData();
        XElement OrderItemID = configRoot.Element("OrderItemID");
        OrderItemID.Value = (Convert.ToInt32(OrderItemID.Value) + 1).ToString();
        configRoot.Save(configPath);
        return (Convert.ToInt32(OrderItemID.Value));
    }
}
