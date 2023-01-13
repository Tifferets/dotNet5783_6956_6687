using BO;
using Microsoft.VisualBasic;
using PL.PlProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
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
        private ObservableCollection<OrderItemPL>? Items { get; set; }
        // private Action<OrderForList> action;
        BO.Order? order;
        //OrderItemPL orderItemPL = new OrderItemPL();
        public OrderWindow(Order? mydata)
        {
            InitializeComponent();
            order = mydata;
            Order_Grid.DataContext = mydata;
            var lst = new List<OrderItemPL>();
            foreach (var x in order?.Items)
            {
                lst.Add(new OrderItemPL() { ID = x.ID, ProductID = x.ProductID, Amount = x.Amount, Name = x.Name, Price = x.Price, TotalPrice = x.TotalPrice });
            }
            // var lst = from x in mydata?.Items
            // select new OrderItemPL() { ID = x.ID, ProductID = x.ProductID, Amount = x.Amount, Name = x.Name, Price = x.Price, Totalprice = x.TotalPrice };
            OrderStatus_comboBox.ItemsSource = OrderStatus.GetValues(typeof(PL.OrderStatus));//combobox source 
            Items = new ObservableCollection<OrderItemPL>(lst.ToList());// mydata.Items);
            Items_listview.DataContext = Items;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Items_listview.SelectedIndex >= 0)
            {
                OrderItemPL? x = Items_listview.SelectedItem as OrderItemPL;//creats a new productforlist
                OrderItem oi = new OrderItem() { ID = x.ID, ProductID = x.ProductID, Amount = x.Amount, Name = x.Name, Price = x.Price, TotalPrice = x.TotalPrice };
                try
                {
                    int amount;
                    //int.TryParse(Interaction.InputBox("Please Enter the amount to change to:", "Update Product in order", ""), out amount);//displays an inputbox
                    string input = Microsoft.VisualBasic.Interaction.InputBox("Please Enter the amount to change to:", "Update Product in order", "", -1, -1);
                    if (!string.IsNullOrEmpty(input))
                    {
                        amount = Convert.ToInt32(input);
                        bl.Order.UpdateOrder(order, amount, oi);
                        x.Amount = amount;
                        x.TotalPrice = oi.Price * amount;
                        //var item = items?.FirstOrDefault(x => x.ID == oi.ID);
                        //int index = items.IndexOf(item);
                        //items[index] = oi;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}
