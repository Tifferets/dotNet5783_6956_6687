using BO;
using PL.PlProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Admin_Window.xaml
    /// </summary>
    public partial class Admin_Window : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        private ObservableCollection<ProductForList> productForLists { get; set; }
        private ObservableCollection<OrderForList> orderForLists { get; set; }
        public Admin_Window()
        {
            InitializeComponent();
            productForLists = new ObservableCollection<ProductForList>(bl?.Product.GetListOfProducts().ToList());
            orderForLists = new ObservableCollection<OrderForList>(bl?.Order.GetOrderList().ToList());
            ProductListview.DataContext = productForLists;
            orderListview.DataContext = orderForLists;

        }
        private void addProduct(ProductForList productForList) => productForLists.Add(productForList);
        private void Button_Click(object sender, RoutedEventArgs e) => new ProductWindow(addProduct).Show();//opens product window
        private void updateProduct(ProductForList productForList)
        {
            var item = productForLists.FirstOrDefault(x => x.ID == productForList.ID);
            int index = productForLists.IndexOf(item);
            productForLists[index] = productForList;
        }
        private void updateOrder(BO.Order order)
        {
            var item = orderForLists.FirstOrDefault(x => x.ID == order.ID);
            int index = orderForLists.IndexOf(item);
            OrderForList ofl1= new OrderForList() { ID= order.ID , AmountOfItems= order.Items.Count, CustomerName= order.CustomerName, Status= order.Status, TotalPrice= order.TotalPrice};
            orderForLists[index] = ofl1;
        }
        private void MouseDoubleClickedProduct(object sender, MouseButtonEventArgs e)
        {
            if (ProductListview.SelectedIndex >= 0)
            {
                ProductForList? p1 = (ProductListview.SelectedItem as ProductForList);//creats a new productforlist
                Product product = bl?.Product.GetProductbyID(p1.ID);
                if (product != null)
                {
                    new ProductWindow(updateProduct, product).ShowDialog();

                }
            }
        }

        private void MouseDoubleClickedOrder(object sender, MouseButtonEventArgs e)
        {
            if (orderListview.SelectedIndex >= 0)
            {
                OrderForList? p1 = orderListview.SelectedItem as OrderForList;//creats a new productforlist
                Order? order = bl?.Order.GetOrderInfo(p1.ID);
                if (p1 != null)
                {
                    OrderWindow orderWindow = new OrderWindow(order,updateOrder);
                    orderWindow.ShowDialog();//opens the window with the orders information
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//delete product
        {
            if (ProductListview.SelectedIndex >= 0)
            {
                ProductForList? p1 = ProductListview.SelectedItem as ProductForList;//creats a new productforlist
                try
                {
                    bl?.Product.DeletProduct(p1.ID);
                    productForLists.Remove(p1);
                    MessageBox.Show("Product deleted successfully");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}


