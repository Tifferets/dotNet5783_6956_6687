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

            //Ordered_datePicker.SelectedDate = DateTime.Now;//the date we start with is nows date


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

        //private void Add_button_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (OrderStatus_comboBox.SelectedItem == null) //if they didnt enter a status 
        //        {
        //            MessageBox.Show("please select a status");
        //            return;
        //        }
        //        if( CheckEmail(Email_textBox.Text) == false)
        //        {
        //            MessageBox.Show("Incorrect email format");
        //            return;
        //        }
        //        BO.Order order = new BO.Order()//creating a new order
        //        {
        //            ID = int.Parse(OrderID_textBox.Text),
        //            CustomerName=Name_textbox.Text,
        //            CustomerAddress = Address_textbox.Text,
        //            CustomerEmail = Email_textBox.Text,
        //            TotalPrice =int.Parse(TotalPrice_textbox.Text),
        //            Status = (BO.OrderStatus)OrderStatus_comboBox.SelectedItem,
        //            OrderDate = Ordered_datePicker.SelectedDate,
        //            ShipDate = Shipped_datePicker.SelectedDate,
        //            DeliveryDate= Ordered_datePicker.SelectedDate,

        //        };
        //        try
        //        {
        //          //  bl?.Order.AddOrder(order);//adds the order to the do
        //          //  action(bl?.Order.GetProductForList(order.ID) ?? throw new NullException());
        //            MessageBox.Show("Order added successfully");
        //            this.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //    catch (Exception)//if missing any data
        //    {
        //        MessageBox.Show("Please enter missing data");
        //    }

        //     }
        //    private bool CheckEmail(string email)
        //    {//returns true if the email is proper else returns false
        //        if (email == null) return false;
        //        if (email.Contains('@')) return true;
        //        return false;
        //    }
        //    #region correct input
        //    private void PreviewTextImputString(object sender, TextCompositionEventArgs e)// for name -only lets to put letters 
        //    {
        //        e.Handled = IsTextAllowedString(e.Text);//checks what is there
        //    }
        //    private static readonly Regex regex_str = new Regex("[^A-Z a-z]+");//only lets it be a letter
        //    private static bool IsTextAllowedString(string text) //for name - makes sure the imput is a letter
        //    {
        //        return regex_str.IsMatch(text);
        //    }

        //    private void PreviewTextImput(object sender, TextCompositionEventArgs e)// for price- only lets to put numbers, can with decimal 
        //    {
        //        e.Handled = !IsTextAllowed(e.Text);
        //    }
        //    private static readonly Regex _regex = new Regex("[^0-9.]+"); //regex that matches disallowed text -only positive and dec
        //    private static bool IsTextAllowed(string text) //for price
        //    {
        //        return !_regex.IsMatch(text);
        //    }

        //    private void PreviewTextImputNoDec(object sender, TextCompositionEventArgs e)// for InStock and id- only lets to put numbers, without decimal 
        //    {
        //        e.Handled = !IsTextAllowedDec(e.Text);
        //    }
        //    private static readonly Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text-only positive no decimal
        //    private static bool IsTextAllowedDec(string text)// for in stock and id
        //    {
        //        return !regex.IsMatch(text);
        //    }
        //    #endregion
        //}
        // }
    }
}
