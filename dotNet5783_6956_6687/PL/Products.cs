using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Data;
using System.Windows.Navigation;

namespace PL.PlProduct;

public class Products : INotifyPropertyChanged
{
    private int Id;
    private double price;
    private Category category;
    private int amount;
    private bool inStock;
    public int ID
    {
        get { return Id; }
        set
        {
            Id = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
        }

    }

    private string? name;
    public string? Name
    {
        get { return name; }
        set
        {
            name = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
    }
    public Category Category
    {
        get { return category; }
        set
        {
            category = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Category"));
            }
        }

    }
    public double Price
    {
        get { return price; }
        set
        {
            price = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Price"));
            }
        }

    }
    public int Amount
    {
        get { return amount; }
        set
        {
            amount = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
            }
        }

    }
    public bool InStock
    {
        get { return inStock; }
        set
        {
            inStock = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("InStock"));
            }
        }

    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class Cart : INotifyPropertyChanged
{
    private double totalprice;
    public double TotalPrice
    {
        get { return totalprice; }
        set
        {
            totalprice = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
            }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
}
public class Checkout : INotifyPropertyChanged
{
    private bool _checkedout;
    public bool Checkedout
    {
        get { return _checkedout; }
        set
        {
            _checkedout = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Checkedout"));
            }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class OrderItemPL : INotifyPropertyChanged
{
    private int Id;
    private string name;
    private double price;
    private int productID;
    private int amount;
    private double totalprice;
    
    public int ID
    {
        get { return Id; }
        set
        {
            Id = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
        }

    }

    public string? Name
    {
        get { return name; }
        set
        {
            name = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
    }
    public int ProductID
    {
        get { return productID; }
        set
        {
            productID = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ProductID"));
            }
        }

    }


    public double Price
    {
        get { return price; }
        set
        {
            price = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Price"));
            }
        }

    }
    public int Amount
    {
        get { return amount; }
        set
        {
            amount = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
            }
        }

    }
    public double TotalPrice
    {
        get { return totalprice; }
        set
        {
            totalprice = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
            }
        }

    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
public class OrderPL : INotifyPropertyChanged
{
    private double totalprice;
    public double TotalPrice
    {
        get { return totalprice; }
        set
        {
            totalprice = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
            }
        }

    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
