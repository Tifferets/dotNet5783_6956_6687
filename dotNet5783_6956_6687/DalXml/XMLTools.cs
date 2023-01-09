using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
//gjgj
namespace Dal
{
    class XMLTools
    {
        static string dir = @"xml\";
        static XMLTools()
        {
            if(!Directory.Exists(dir))
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
                     //  throw new DO.CantSaveListToXMLException();
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
                    if (list != null)
                        return list;
                    else
                        throw new DalApi.EmptyListException();
                }
                else
                    return new List<T>();
            }
            catch(Exception ex)
            {
                throw new DalApi.CantGetListFromXMLException();
            }
        }
    }
}
/*
     LoadData();
     List<DO.Order> OrderList =Dal.XMLTools.LoadListFromXML<DO.Order>(fPath); //gets all the orders
     Order or = OrderList.FirstOrDefault(x => x.ID == order.ID);//or is A copy of the order we  want to delete
     OrderList.Remove(or);//removes the order from the list
     Dal.XMLTools.SaveListToXML(OrderList, fPath);
   */
