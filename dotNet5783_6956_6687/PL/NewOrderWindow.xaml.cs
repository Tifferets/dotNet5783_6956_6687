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
using System.Collections;
using BlApi;

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
        private BO.Cart Cart = new BO.Cart();
        private Products ProductItem;
        public NewOrderWindow(BO.Cart cart = null , Products productItem = null) : this() 
        {
            this.refresh(cart, productItem);
        }
        public NewOrderWindow()
        {
            InitializeComponent();
            Category_ComboBox.ItemsSource = Category.GetValues(typeof(PL.Category));//combobox source 
            productItemList = new ObservableCollection<ProductItem?>(bl.Product.GetlListOfProductItem().ToList());
            ProductItemWindow_listView.DataContext = productItemList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(productItemList);//categorizes the list 
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
            view.GroupDescriptions.Add(groupDescription);
        }
        private void Category_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CatergoryGroup = new ObservableCollection<IGrouping<BO.Category, ProductItem>>  //grouping of products by category
                            (from item in productItemList
                             orderby item.Category
                             group item by item.Category into item
                             select item);
            ObservableCollection<ProductItem?> temp = new ObservableCollection<ProductItem?>(productItemList);

            if (Category_ComboBox.SelectedItem != null && Category_ComboBox.SelectedItem is not Category.All) //we want to chang the info
            {
                temp = new ObservableCollection<ProductItem?>(CatergoryGroup[Category_ComboBox.SelectedIndex]);
                ProductItemWindow_listView.DataContext = temp;
            }

            else if (Category_ComboBox.SelectedItem is Category.All)//if we selected all
            {
              //productItemList = new ObservableCollection<ProductItem?>(productItemList);
                ProductItemWindow_listView.DataContext = productItemList;
                Category_ComboBox.ItemsSource = Category.GetValues(typeof(PL.Category));//combobox source
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)//go to cart button
        { 
            new CartWindow1(Cart, refresh).ShowDialog();
            ProductItemWindow_listView.DataContext = productItemList;
        }
        private void MouseDoubleClicked(object sender, MouseButtonEventArgs e)//double click on the product to view it
        {
            if (ProductItemWindow_listView.SelectedIndex >= 0)
            {
                try
                {

                    ProductItem? p1 = ProductItemWindow_listView.SelectedItem as ProductItem;//get the selected item
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
                       // this.Close();
                    }
                    Category_ComboBox.SelectedItem = Category.All;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
      
        private void refresh(BO.Cart cart , Products productItem)//func that refreshes the datacontext of window 
        {
            if (productItem != null)//if the product item isnt null we want to update it in our CO
            {
                ProductItem? product;
                if (productItem.Name == null)//if it came here by changing  product from the cart
                {
                    product = productItemList.FirstOrDefault(x => x?.ID == productItem.ID);
                    product.Amount = productItem.Amount;
                }
                else//came here from adding / removing from productitem window
                { 
                    product = new ProductItem()
                    {
                        ID = productItem.ID,
                        Name = productItem.Name,
                        Amount = productItem.Amount, //was zer0 i changed
                        Category = (BO.Category)productItem.Category,
                        Instock = productItem.InStock,
                        Price = productItem.Price
                    };
                }
                var item = productItemList.FirstOrDefault(x => x?.ID == productItem.ID);
                var index = productItemList.IndexOf(item);
                productItemList[index] = product;
            }
            ProductItemWindow_listView.DataContext = productItemList;
        } 
    }
}

