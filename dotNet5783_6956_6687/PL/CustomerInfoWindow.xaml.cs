using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerInfoWindow.xaml
    /// </summary>
    public partial class CustomerInfoWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        Cart cart1 = new Cart();
        public CustomerInfoWindow()
        {
            InitializeComponent();
        }
        public CustomerInfoWindow(Cart cart) : this()
        {
            if (cart.Items == null) 
            {
                MessageBox.Show("Cart is empty, Please add product befor you check out");
                this.Close();
            }
            cart1 = cart;
        }

        private void CheckOut_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool good = Email_TextBox.Text.Contains('@');//makes sure is a correct email
                if (Address_TextBox.Text == "" || Email_TextBox.Text == "" || Name_TextBox.Text == ""|| Email_TextBox.Text == "Customer@gmail.com")
                    MessageBox.Show("add missing data");
                // if( good != true)//wrong email
                else
                {
                    Cart cart = new Cart()//creats the  customers cart
                    {
                        CustomerAddress = Address_TextBox.Text,
                        CustomerEmail = Email_TextBox.Text,
                        CustomerName = Name_TextBox.Text,
                        Items = cart1.Items.ToList(),
                        TotalPrice = cart1.TotalPrice,

                    };
                    bl?.Cart.confirmCart(cart);
                    MessageBox.Show("Thank you and have a nice day");
                    this.Close();
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
    }
}
