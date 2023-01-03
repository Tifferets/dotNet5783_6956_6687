using BO;
using PL.PlProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<OrderForList> orderForLists{ get; set; }
        public Admin_Window()
        {
            InitializeComponent();
            productForLists = new ObservableCollection<ProductForList>(bl?.Product.GetListOfProducts().ToList());
            orderForLists = new ObservableCollection<OrderForList>(bl?.Order.GetOrderList().ToList());
            ProductListview.DataContext = productForLists;
            orderListview.DataContext = orderForLists;
        }
        private void addProduct(ProductForList productForList)=> productForLists.Add(productForList);
        private void Button_Click(object sender, RoutedEventArgs e) => new ProductWindow(addProduct).Show();//opens product window
        private void updateProduct(ProductForList productForList)
        {
            var item = productForLists.FirstOrDefault(x=> x.ID == productForList.ID);
            int index= productForLists.IndexOf(item);
            productForLists[index] = productForList;
        }

       // private void addOrder(OrderForList orderForList) => orderForLists.Add(orderForList);
        //private void Button_Click_1(object sender, RoutedEventArgs e) => new OrderWindow(addOrder).ShowDialog();//opens order window
        private void MouseDoubleClickedProduct(object sender, MouseButtonEventArgs e)
        {
            if (ProductListview.SelectedIndex >= 0)
            {
                ProductForList? p1 = (ProductListview.SelectedItem as ProductForList);//creats a new productforlist
                Product product = bl?.Product.GetProductbyID(p1.ID);
                if (product != null)
                {
                    //ProductWindow productWindow = new ProductWindow(p1);
                    //productWindow.ShowDialog();
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
                    OrderWindow orderWindow = new OrderWindow(order);
                    orderWindow.ShowDialog();//opens the window with the orders information
                }
            }
        }


    }
}
