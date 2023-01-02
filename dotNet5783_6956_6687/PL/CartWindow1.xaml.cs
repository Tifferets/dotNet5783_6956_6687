using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CartWindow1.xaml
    /// </summary>
    public partial class CartWindow1 : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        private ObservableCollection<OrderItem> OrderItemList { get; set; }
        Cart cart1= new Cart();
        public CartWindow1()
        {
            InitializeComponent();
        }
        public CartWindow1(Cart cart = null):this()
        {
            cart1 = cart;
            if(cart.Items != null) 
            {
                OrderItemList = new ObservableCollection<OrderItem>(cart.Items);
                Products_DataGrid.DataContext = OrderItemList;
                this.DataContext = cart1;
            }
            else
            {
                OrderItemList = new ObservableCollection<OrderItem>();
            }
            cart1 = cart;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerInfoWindow ciw = new CustomerInfoWindow(cart1);
            ciw.ShowDialog();
            //try
            //{
            //    cart1.CustomerEmail =cu

            //    bl.Cart.confirmCart(cart1,)
            //   // BO.Cart cart = new BO.Cart()
                

            //}
            //catch(Exception ex) { MessageBox.Show(ex.ToString()); }//if theres a problem 
           
            
            MessageBox.Show("Thank you and have a nice day");
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)=> this.Close();

    }
}
