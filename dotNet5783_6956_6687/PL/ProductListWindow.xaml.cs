using BlApi;
using BO;
using PL.PlProduct;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PL
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
            Category_ComboBox.ItemsSource = Category.GetValues(typeof(BO.Category));//combobox source of info- categories
            
        }
        private void Category_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)//combobox selection of category- changes listView to that category
        {
            ProductListView.ItemsSource = bl.Product.GetproductForListByCategory((Category)Category_ComboBox.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)//botten to open add product 
        {
            ProductWindow productWindow = new ProductWindow();
            productWindow.ShowDialog();//botten to open add product 
            ProductListView.ItemsSource = bl.Product.GetListOfProducts();//listveiws source from BO func getLstOfProducts
        }
        
        private void ProductListView_MouseDoubleClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)//listView double tap to update product
        {
            ProductWindow productWindow = new ProductWindow((BO.ProductForList)ProductListView.SelectedItem);//opens other window with constructer that gets a product and puts the data in the textBoxes
            productWindow.ShowDialog();
            ProductListView.ItemsSource = bl.Product.GetListOfProducts();//listveiws source from BO func getLstOfProducts

        }

        //private void ProductListView_MouseDoubleClicked(object sender, MouseButtonEventArgs e)
        //{
        //    //myData = (BO.ProductForList)ProductListView.SelectedItem
        //    ProductWindow productWindow = new ProductWindow((BO.ProductForList)ProductListView.SelectedItem);         
        //   // productWindow.myData = (BO.ProductForList)ProductListView.SelectedValue;
        //    productWindow.ShowDialog();
        //}
    }
}
