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
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        Cart Cart = new Cart()
        {
            Items = new List<OrderItem>()
        };
        ProductItem ProductItem = new ProductItem();
        public ProductItemWindow()
        {
            InitializeComponent();
        }
        public ProductItemWindow(Cart cart, ProductItem productItem ):this() 
        {
            ProductOtemGrid.DataContext = productItem;
            Cart = cart;
            ProductItem = productItem;
        }

        private void addToCart_Click(object sender, RoutedEventArgs e)
        {
            if (Cart == null) return;
            try
            {
                bl?.Cart.AddProductToCart(Cart, ProductItem.ID);
                MessageBox.Show("Product added successfully");
                this.Close();
                new NewOrderWindow(Cart).ShowDialog();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
