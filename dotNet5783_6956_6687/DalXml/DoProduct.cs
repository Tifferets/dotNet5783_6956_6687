using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal;

public class DoProduct : IProduct
{
    XElement productRoot;
    string FPath = @"Products.xml";
    public DoProduct()
    {
        if (File.Exists(FPath) == false)
            CreatFile();
        else
            LoadData();
    }
    private void CreatFile()
    {
        productRoot = new XElement("Product");
        productRoot.Save(FPath);
    }
    private void Save(IEnumerable<Product> products)//saves ower list into the xml file
    {
        try 
        {
            productRoot = new XElement("Products", from p in products
                                                   select new XElement("Product", new XElement("ID", p.ID),
 
                                                   new XElement("Name", p.Name),
 
                                                   new XElement("Category", p.Category),
 
                                                   new XElement("InStock", p.InStock),
 
                                                   new XElement("Price", p.Price)));

            productRoot.Save(FPath);
        }
        catch (Exception ex)
        { 
            throw ex;
        }
    }
    private void LoadData()
    {
        try
        {
            productRoot =XElement.Load(FPath);
        }
        catch
        {
            throw new Exception("file upload product nut successful");
        }
    }
    public Product? GetSingle(Func<Product?, bool> func)
    {
        //gets a single product from the file
        LoadData();
        IEnumerable<Product?> products;
        List<Product> plist;
        try
        {
            plist = (from p in productRoot.Elements()
                     select new Product()
                     {
                         ID = Convert.ToInt32(p.Element("ID").Value),
                         Name = p.Element("Name").Value,
                         InStock = Convert.ToInt32(p.Element("InStock").Value),
                         Category = (DO.Category)Enum.Parse(typeof(DO.Category), p.Element("Category").Value),
                         Price = Convert.ToInt32(p.Element("Price").Value)
                     }).ToList();
            productRoot.Save(FPath);
            products = (IEnumerable<Product?>)plist;
            Product? p1 = products.FirstOrDefault(func);
            return p1;
        }
        catch 
        {
            throw new Exception("cant get single");
        }
       

    } 
    public IEnumerable<Product?> GetAll(Func<Product?, bool> func)
    {
        LoadData();
        IEnumerable<Product?> products;
        List<Product> plist;
        try
        {
            plist = (from p in productRoot.Elements()
                        select new Product()
                        {
                            ID = Convert.ToInt32(p.Element("ID").Value),
                            Name = p.Element("Name").Value,
                            InStock = Convert.ToInt32(p.Element("InStock").Value),
                            Category = (DO.Category)Enum.Parse(typeof(DO.Category), p.Element("Category").Value),
                            Price = Convert.ToInt32(p.Element("Price").Value)

                        }).ToList();
            products = (IEnumerable<Product?>)plist;
            if (func == null)
            {
                return products;
            }
            else
            {
                return products.Where(func).OrderByDescending(x => x?.ID);
            }
               
        }
        catch
        {
            products = null;
        }
        return products;
    }
    public int Add(Product product)//gets a product and adds it to the xml file
    {
        if (product.ID < 300000 || product.ID > 400000)
            throw new WrongIdException();
        if(product.Name == null)
            throw new NoNameException();
        try
        {
            LoadData();
            XElement ID = new XElement("ID",product.ID);
            XElement Name = new XElement("Name", product.Name);
            XElement Category = new XElement("Category", product.Category);
            XElement Price = new XElement("Price", product.Price);
            XElement InStock = new XElement("InStock", product.InStock);
            productRoot.Add(new XElement("Product", ID, Name, Category, Price, InStock));
            productRoot.Save(FPath);


        }
        catch(Exception ex)
        {
            //throw "Cant add product";
        }
        return product.ID;
    }
    public void Delete(int id)
    {
        try
        {
            LoadData();
            XElement product;
            product = (from p in productRoot.Elements()
                     where Convert.ToInt32(p.Element("ID").Value) ==id
                     select p).FirstOrDefault();
            product.Remove();
            productRoot.Save(FPath);
            return;

        }
        catch
        {
           // throw "Cant delete product";
        }
    }
    public void Update(Product product)
    {
        try
        {
            LoadData();
            XElement productElment = (from p in productRoot.Elements()
                                      where Convert.ToInt32(p.Element("ID").Value) == product.ID
                                      select p).FirstOrDefault();

            productElment.Element("Name").Value = product.Name;
            productElment.Element("Price").Value = product.Price.ToString();
            productElment.Element("Category").Value = product.Category.ToString();
            productElment.Element("InStock").Value = product.InStock.ToString();
            productRoot.Save(FPath);
        }
        catch
        {

        }
    }
}
