using BO;
using PL.PlProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for CustomerInfoWindow.xaml
/// </summary>

public partial class CustomerInfoWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    BO.Cart cart1 = new BO.Cart();
    Checkout checkout = new Checkout();

    public CustomerInfoWindow()
    {
        InitializeComponent();
        checkout.Checkedout = false;
    }
    public CustomerInfoWindow(BO.Cart cart) : this()
    {

        cart1 = cart;
    }

    private void CheckOut_button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bool good = Email_TextBox.Text.Contains('@');//makes sure is a correct email
            if (Address_TextBox.Text == "" || Email_TextBox.Text == "" || Name_TextBox.Text == "" || Email_TextBox.Text == "Customer@gmail.com")
                MessageBox.Show("add missing data");
            if (Email_TextBox.Text.Count(c => c == '@') > 1 || good == false)
            {
                MessageBox.Show("Email address not right");
                return;
            }
            else
            {
                BO.Cart cart = new BO.Cart()//creats the  customers cart
                {
                    CustomerAddress = Address_TextBox.Text,
                    CustomerEmail = Email_TextBox.Text,
                    CustomerName = Name_TextBox.Text,
                    Items = cart1.Items.ToList(),
                    TotalPrice = cart1.TotalPrice,

                };
                bl?.Cart.confirmCart(cart);
                checkout.Checkedout = true;
                MessageBox.Show("Thank you and have a nice day");
                CloseAllWindows();//closes all the window

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message.ToString());
            this.Close();
        }
    }
    private void PreviewTextImputString(object sender, TextCompositionEventArgs e)// for name -only lets to put letters 
    {
        e.Handled = IsTextAllowedString(e.Text);//checks what is there
    }
    private static readonly Regex regex_str = new Regex("[^A-Z a-z]+");//only lets it be a letter
    private static bool IsTextAllowedString(string text) //for name - makes sure the imput is a letter
    {
        return regex_str.IsMatch(text);
    }
    private void CloseAllWindows()
    {
        for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
        {
         if(!(App.Current.Windows[intCounter] is SimulatorWindow ))
            App.Current.Windows[intCounter].Close();
    }
}
