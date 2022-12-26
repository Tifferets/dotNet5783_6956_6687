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
        public ObservableCollection<OrderTracking> orderTrackings= new ObservableCollection<OrderTracking>();
        public TrackOrder_Window()
        {
            InitializeComponent();
            this.orderTrackings = new ObservableCollection<OrderTracking>();
            TrackOrderList.DataContext = (from OrderForList? item in bl?.Order.GetOrderList()
                                          select (bl.Order.OrderStatus(item.ID)));
        }

        private void TrackOrderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
