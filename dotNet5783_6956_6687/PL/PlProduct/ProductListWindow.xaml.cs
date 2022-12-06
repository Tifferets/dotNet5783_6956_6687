using BlApi;
using BO;
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

namespace PL.PlProduct
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl = new BlImplementation.Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetListOfProducts();//listveiws source from BO func getLstOfProducts
            categoryComboBox.ItemsSource = Category.GetValues(typeof(BO.Category));//combobox source of info- categories
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductListView.ItemsSource = bl.Product.GetproductForListByCategory((Category)categoryComboBox.SelectedItem);
            //Func<BO.Category, bool> func => {Category == categoryComboBox.SelectedItem};
            //ProductListView.ItemsSource = bl.Product.GetListOfProducts(func);

            ProductListView.ItemsSource = bl.Product.GetListOfProducts();

        }
    }
}
