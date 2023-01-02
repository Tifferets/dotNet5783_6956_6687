using BO;
using Microsoft.VisualBasic;
using PL.PlProduct;
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
        private ObservableCollection<ProductItem?> productItemList { get; set; }
        public ObservableCollection<IGrouping<BO.Category, ProductItem>> CatergoryGroup { get; set; }
        private Cart Cart = new Cart();
        private Products ProductItem { get; set; }
        public NewOrderWindow(Cart cart = null , ProductItem productItem = null) : this() 
        {
            Cart = cart;
            ProductItem = new Products()
            {
                ID = productItem.ID,
                Name = productItem.Name,
                Amount = productItem.Amount,
                Category = (PL.Category)productItem.Category,
                InStock = productItem.Instock,
                Price = productItem.Price,
            };
        }
        public NewOrderWindow()
        {
            InitializeComponent();
            Category_ComboBox.ItemsSource = Category.GetValues(typeof(PL.Category));//combobox source 
            productItemList = new ObservableCollection<ProductItem?>(bl.Product.GetlListOfProductItem().ToList());
            // List<ProductItem> lst = productItemList.OrderBy(x => x.Category.ToString()).ToList();
            ProductItem_DataGrid.DataContext = productItemList;
            // CatergoryGroup = CatergoryGroup.
            CatergoryGroup = new ObservableCollection < IGrouping < BO.Category, ProductItem >>
                (from item in bl.Product.GetlListOfProductItem()
                orderby item.Category
                group item by item.Category into item
                select item);

        }
        private void Category_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Category_ComboBox.SelectedItem != null && Category_ComboBox.SelectedItem is not Category.All) //we want to chang the info
            {
                productItemList = new ObservableCollection<ProductItem?>(CatergoryGroup[Category_ComboBox.SelectedIndex]);
                ProductItem_DataGrid.DataContext = productItemList;
            }

            else if (Category_ComboBox.SelectedItem is Category.All)
            {
                productItemList = new ObservableCollection<ProductItem?>(bl.Product.GetlListOfProductItem().ToList());
                ProductItem_DataGrid.DataContext = productItemList;
                Category_ComboBox.ItemsSource = Category.GetValues(typeof(PL.Category));//combobox source
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) => new CartWindow1(Cart).ShowDialog();
        private void MouseDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            if (ProductItem_DataGrid.SelectedIndex >= 0)
            {
                try
                {
                    ProductItem? p1 = ProductItem_DataGrid.SelectedItem as ProductItem;
                    if (p1 != null)
                    {
                        this.Close();
                        new ProductItemWindow(Cart, p1).ShowDialog();
                    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Prosuct is out of stock, sorry");
                    //}}
                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void ShopBy_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ProductItem_DataGrid.DataContext = CatergoryGroup;
        }
    }
}
           


