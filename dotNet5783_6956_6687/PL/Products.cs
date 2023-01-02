using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace PL.PlProduct;

public class Products: INotifyPropertyChanged
{
    private int Id;
   
    private double price;
    private Category category;
    private int amount;
    private bool inStock;
    public int ID
    {
        get { return Id;}
        set
        { 
            Id = value; 
            if(PropertyChanged != null)
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
}
