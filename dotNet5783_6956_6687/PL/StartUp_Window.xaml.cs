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
using BO;
using Microsoft.VisualBasic;
using PL.PlProduct;

namespace PL
{
    /// <summary>
    /// Interaction logic for StartUp_Window.xaml
    /// </summary>
    public partial class StartUp_Window : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart = new BO.Cart();

        public StartUp_Window()
        {
            InitializeComponent();
        }

        private void Next_button_Click(object sender, RoutedEventArgs e)
        {
            if (Admin.IsChecked == true)
            {
                new Admin_Window().ShowDialog();
            }
            else if (NewOrder.IsChecked == true)
            {
                new NewOrderWindow(cart).ShowDialog();
            }
            else if (TrackOrder.IsChecked == true)
            {
                int value;
                int.TryParse(Interaction.InputBox("Please Enter Order ID To Track An Order", "Tracking Order ID", "100000"), out value);//displays an inputbox and gets the id
                try
                {
                    if (value != 0)//making sure there is text
                    {
                        OrderTracking? orderTracking = bl?.Order.OrderStatus(value);
                        if (orderTracking != null)//checking thet there is an order with the id
                        {
                            new TrackOrder_Window(value).ShowDialog();//opens the window with the id
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
