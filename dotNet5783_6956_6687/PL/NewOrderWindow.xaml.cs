using BO;
using DO;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
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
            if (Category_ComboBox.SelectedItem != null && Category_ComboBox.SelectedItem is not Category.All) //
                ProductItem_DataGrid.DataContext = bl?.Product.GetproductForListByCategory((BO.Category)Category_ComboBox.SelectedItem);
            else if (Category_ComboBox.SelectedItem is Category.All)
            {
                ProductItem_DataGrid.DataContext = bl?.Product.GetListOfProducts();//listveiws source from BO func getLstOfProducts
                Category_ComboBox.ItemsSource = Category.GetValues(typeof(PL.Category));//combobox source
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CartWindow1 c1= new CartWindow1();
            var vm = Application.Current.Windows[0].DataContext;
            c1.DataContext= vm;
            c1.ShowDialog();

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
