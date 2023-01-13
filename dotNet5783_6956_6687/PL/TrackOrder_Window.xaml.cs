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
    /// Interaction logic for TrackOrder_Window.xaml
    /// </summary>
    public partial class TrackOrder_Window : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<OrderTracking> orderTrackings { get; set; }
        public TrackOrder_Window()
        {
            InitializeComponent();
            orderTrackings = new ObservableCollection<OrderTracking>();
        }
        public TrackOrder_Window(int value):this()
        {
            TrackOrder_Grid.DataContext = bl?.Order.OrderStatus(value);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//opens the order window details 
        {
            try
            {
                int id;
                int.TryParse(Id_textbox.Text, out id);
                Order? order = bl?.Order.GetOrderInfo(id);
                new OrderWindow(order).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
