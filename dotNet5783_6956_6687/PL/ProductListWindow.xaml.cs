using BlApi;
using BlImplementation;
using BO;
using PL.PlProduct;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl = new BlImplementation.Bl();
        public BO.ProductForList? myData { get; set; }
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetListOfProducts();//listveiws source from BO func getLstOfProducts
            Category_ComboBox.ItemsSource = Category.GetValues(typeof(BO.Category));//combobox source of info- categories
            
        }
        private void Category_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductListView.ItemsSource = bl.Product.GetproductForListByCategory((Category)Category_ComboBox.SelectedItem);
           // ProductListView.ItemsSource = bl.Product.GetListOfProducts();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show();

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductListView_MouseDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            //myData = (BO.ProductForList)ProductListView.SelectedItem
            ProductWindow productWindow = new ProductWindow();
            productWindow.myData = (BO.ProductForList)ProductListView.SelectedItem;
            productWindow.ShowDialog();
        }
    }
}
