using BO;
using DO;
using Microsoft.VisualBasic;
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
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>


    public partial class NewOrderWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        private ObservableCollection<ProductItem> productItemList { get; set; }
        public NewOrderWindow()
        {
            InitializeComponent();
            Category_ComboBox.ItemsSource = Category.GetValues(typeof(PL.Category));//combobox source 
            productItemList = new ObservableCollection<ProductItem>(bl.Product.GetlListOfProductItem().ToList());
            List<ProductItem> lst=   productItemList.OrderBy(x => x.Category.ToString()).ToList();
            ProductItem_DataGrid.DataContext = lst;
            //Category_ComboBox.SelectedItem = Category.All;
        }

        private void Category_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var groups = productItemList.GroupBy(x => x.Category);
            //var products = (from x in groups
            //                             where x.Key == (BO.Category)Category_ComboBox.SelectedItem
            //                             select x).ToList();
            //ProductItem_DataGrid.DataContext = products.va
            // productItemList = new ObservableCollection<ProductItem>();
            // ProductItem_DataGrid.DataContext = groups.Where(x => x?.Key.ToString() == Category_ComboBox.SelectedItem);

            //productItemList = from ProductItem item in productItemList
            //                  let choice = Category_ComboBox.SelectedItem
            //                  group item by item.Category into lst
            //                  select new { key = lst.Key, item = lst }
        }

        private void Button_Click(object sender, RoutedEventArgs e) => new CartWindow1().ShowDialog();
        //{
            //int value;
            //int.TryParse(Interaction.InputBox("Please Enter Name, Email, Address", "Tracking Order ID", "100000"), out value);//displays an inputbox and gets the id
            //try
            //{
            //    if (value != 0)//making sure there is text
            //    {
            //        OrderTracking? orderTracking = bl?.Order.OrderStatus(value);
            //        if (orderTracking != null)//checking thet there is an order with the id
            //        {
            //            new TrackOrder_Window(value).ShowDialog();//opens the window with the id
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
      //  }
    }
}
