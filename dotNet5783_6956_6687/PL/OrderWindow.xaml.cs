using BO;
using DO;
using Microsoft.VisualBasic;
using PL.PlProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Navigation;
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
        private Action<BO.Order> action;
        BO.Order order;
        OrderItemPL orderItemPL = new OrderItemPL();// inotifiableproperty 
        OrderPL Orderpl = new OrderPL();//inotifiableproperty for the total price of the order
        public OrderWindow(BO.Order? mydata, Action<BO.Order> action = null) : this()
        {
            if (action != null)
                this.action = action;
            order = mydata;
            Orderpl.TotalPrice = order.TotalPrice;
            Order_Grid.DataContext = mydata;
            var lst = from x in mydata?.Items
                      select new OrderItemPL() { ID = x.ID, ProductID = x.ProductID, Amount = x.Amount, Name = x.Name, Price = x.Price, TotalPrice = x.TotalPrice };
           // OrderStatus_comboBox.ItemsSource = OrderStatus.GetValues(typeof(PL.OrderStatus));//combobox source 
            Items = new ObservableCollection<OrderItemPL>(lst.ToList());// mydata.Items);
            Items_listview.DataContext = Items;
            Items_listview.SelectionChanged += Items_listview_SelectionChanged;
            TotalPrice_textbox.DataContext = Orderpl;
        }
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
                BO.OrderItem oi = new BO.OrderItem() { ID = x.ID, ProductID = x.ProductID, Amount = x.Amount, Name = x.Name, Price = x.Price, TotalPrice = x.TotalPrice };
                try
                {
                    int amount;
                    string input = Microsoft.VisualBasic.Interaction.InputBox("Please Enter the amount to change to:", "Update Product in order", "", -1, -1);
                    //input box that defalt values are -1
                    if (!string.IsNullOrEmpty(input))//making sure they didnt press cancle 
                    {
                        amount = Convert.ToInt32(input);

                        if (amount >= 0)//making sure the amount isnt negative
                        {
                            if (Items?.Count() == 1 && amount == 0)
                            {
                                MessageBox.Show("There are not enough items in order to remove this item.");
                            }
                            else
                            {
                                bl?.Order.UpdateOrder(order, amount, oi);//updates the amount 
                                x.Amount = amount;//updating for the infotifi...
                                Orderpl.TotalPrice -= x.TotalPrice;
                                Orderpl.TotalPrice += x.TotalPrice = oi.Price * amount;
                                order.TotalPrice = Orderpl.TotalPrice;
                                if (x.Amount == 0)//if the new amounts is 0 we remove the item
                                {
                                    Items.Remove(x);
                                }
                                action(order);
                            }
                        }
                        else
                        {
                            MessageBox.Show("cant put a negative number");
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        private void Items_listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //if the action is null- we came from tracking window as customer and we cant update the items
        {
            if (action == null)
            {
                Items_listview.SelectedItem = null;//it doesnt let you select an item
                Items_listview.SelectedIndex = -1;
            }
        }
        private void Items_listview_MouseEnter(object sender, MouseEventArgs e)
        {
            if (action != null)
            {
                selectItem_label.Visibility = Visibility.Visible;
            }
        }
        private void Items_listview_MouseLeave(object sender, MouseEventArgs e)
        {
            selectItem_label.Visibility = Visibility.Hidden;
        }
    }
}