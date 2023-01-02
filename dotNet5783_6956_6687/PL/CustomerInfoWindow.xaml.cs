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
        Cart Cart = new Cart();
        public CustomerInfoWindow()
        {
            InitializeComponent();
        }
        public CustomerInfoWindow(Cart cart) : this()
        {
            if (cart.Items == null) {
                MessageBox.Show("Cart is empty, Please add product befor you check out");
                this.Close();
            }
        }
    }
}
