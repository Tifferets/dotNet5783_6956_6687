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
        private Products ProductItem;
        public NewOrderWindow(Cart cart = null , Products productItem = null) : this() 
        {
            this.refresh(cart, productItem);
            //Cart = cart;
            //if (productItem != null)//if the product item isnt null we want to update it in our CO
            //{
            //    ProductItem = productItem;
            //    var product = new ProductItem()
            //    {
            //        ID = ProductItem.ID,
            //        Name = ProductItem.Name,
            //        Amount = 0,
            //        Category = (BO.Category)ProductItem.Category,
            //        Instock = ProductItem.InStock,
            //        Price = ProductItem.Price
            //    };
            //    var item = productItemList.FirstOrDefault(x => x.ID == ProductItem.ID);
            //    var index = productItemList.IndexOf(item);
            //    product.Amount = ProductItem.Amount;
            //    productItemList[index] = product;
            //}
            //ProductItem_DataGrid.DataContext = productItemList;
        }
        public NewOrderWindow()
        {
            InitializeComponent();
            Category_ComboBox.ItemsSource = Category.GetValues(typeof(PL.Category));//combobox source 
            productItemList = new ObservableCollection<ProductItem?>(bl.Product.GetlListOfProductItem().ToList());
            ProductItem_DataGrid.DataContext = productItemList;
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
        //private void Button_Click(object sender, RoutedEventArgs e) => new CartWindow1(Cart).ShowDialog();
        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            new CartWindow1(Cart).ShowDialog();
            this.Close();
        }
        private void MouseDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            if (ProductItem_DataGrid.SelectedIndex >= 0)
            {
                try
                {

                    ProductItem? p1 = ProductItem_DataGrid.SelectedItem as ProductItem;//get the selected item
                    ProductItem = new Products()//convert it to pl product item
                    {
                        ID = p1.ID,
                        Name = p1.Name,
                        Amount = p1.Amount,
                        Category = (PL.Category)p1.Category,
                        InStock = p1.Instock,
                        Price = p1.Price,
                    };
                    if (ProductItem != null)//if it exests
                    {
                        new ProductItemWindow(Cart, ProductItem, refresh).ShowDialog();//go to the next window with our cart and product
                    }
                }
                catch(Exception ex)
                { 
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ShopBy_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ProductItem_DataGrid.DataContext = CatergoryGroup;
        }
        private void refresh(Cart cart , Products productItem)
        {
            if (productItem != null)//if the product item isnt null we want to update it in our CO
            { 
                var product = new ProductItem()
                {
                    ID = productItem.ID,
                    Name = productItem.Name,
                    Amount = 0,
                    Category = (BO.Category)productItem.Category,
                    Instock = productItem.InStock,
                    Price = productItem.Price
                };
                var item = productItemList.FirstOrDefault(x => x.ID == productItem.ID);
                var index = productItemList.IndexOf(item);
                product.Amount = productItem.Amount;
                productItemList[index] = product;
            }
            ProductItem_DataGrid.DataContext = productItemList;
        }
    }
}
           


