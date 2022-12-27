using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        private Action<OrderForList> action;
        public OrderWindow(Order? mydata)
        {
            InitializeComponent();
            Order_Grid.DataContext = mydata;
            OrderStatus_comboBox.ItemsSource = OrderStatus.GetValues(typeof(PL.OrderStatus));//combobox source 
        }
    
        public OrderWindow(Action<OrderForList> action)
        {
            InitializeComponent();
            this.action = action;
            OrderStatus_comboBox.ItemsSource = OrderStatus.GetValues(typeof(PL.OrderStatus));//combobox source 
        }
        public OrderWindow()
        {
            InitializeComponent();
            OrderStatus_comboBox.ItemsSource = OrderStatus.GetValues(typeof(PL.OrderStatus));//combobox source 
        }



        private void Add_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OrderStatus_comboBox.SelectedItem == null) //if they didnt enter a status 
                {
                    MessageBox.Show("please select a status");
                    return;
                }
                BO.Order order = new BO.Order()//creating a new order
                {
                    ID = int.Parse(OrderID_textBox.Text),
                    CustomerName=Name_textbox.Text,
                    CustomerAddress = Address_textbox.Text,
                    CustomerEmail = Email_textBox.Text,
                    TotalPrice =int.Parse(TotalPrice_textbox.Text),
                    Status = (BO.OrderStatus)OrderStatus_comboBox.SelectedItem,
                    OrderDate = Ordered_datePicker.SelectedDate,
                    ShipDate = Shipped_datePicker.SelectedDate,
                    DeliveryDate= Ordered_datePicker.SelectedDate,

                };
                try
                {
                   // action(bl.Order.GetOrderList(order.ID);
                   // bl?.Product.AddProduct(product);//adds the product to the do
                    MessageBox.Show("Order added successfully");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception)//if missing any data
            {
                MessageBox.Show("Please enter missing data");
            }
           
        }
        #region correct input
        private void PreviewTextInputString(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowedString(e.Text);
        }
        private static readonly Regex regex_str = new Regex("[^A-Z a-z]+");//only lets it be a letter
        private static bool IsTextAllowedString(string text) //for name - makes sure the imput is a letter
        {
            return regex_str.IsMatch(text);
        }
        #endregion
    }
}
