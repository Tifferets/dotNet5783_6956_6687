using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        private ObservableCollection<OrderItem>? items { get; set; }
        // private Action<OrderForList> action;
        public OrderWindow(Order? mydata)
        {
            InitializeComponent();

            Order_Grid.DataContext = mydata;
            OrderStatus_comboBox.ItemsSource = OrderStatus.GetValues(typeof(PL.OrderStatus));//combobox source 
            items = new ObservableCollection<OrderItem>(mydata.Items);
            Items_listview.DataContext = items;
        }

        //public OrderWindow(Action<OrderForList> action)
        //{
        //    InitializeComponent();
        //    this.action = action;
        //    OrderStatus_comboBox.ItemsSource = OrderStatus.GetValues(typeof(PL.OrderStatus));//combobox source 
        //    //Ordered_datePicker.SelectedDate = DateTime.Now;//the date we start with is nows date
        //}
        public OrderWindow()
        {
            InitializeComponent();
            OrderStatus_comboBox.ItemsSource = OrderStatus.GetValues(typeof(PL.OrderStatus));//combobox source 
            //Ordered_datePicker.SelectedDate= DateTime.Now;//the date we start with is nows date
        }
    }
}
