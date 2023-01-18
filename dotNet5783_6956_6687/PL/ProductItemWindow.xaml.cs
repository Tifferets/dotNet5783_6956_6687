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
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        Action<BO.Cart, Inotify>? action;
        BO.Cart Cart = new BO.Cart()
        {
            Items = new List<OrderItem>()
        };

        Inotify ProductItem = new Inotify();//inotifiableproperty
        public ProductItemWindow()
        {
            InitializeComponent();
        }
        public ProductItemWindow(BO.Cart cart, Inotify p1, Action<BO.Cart, Inotify>? action) : this() //ctor
        {
            this.action = action;
            Cart = cart;
            ProductItem = new Inotify()//builds new productitem- the pl one
            {
                ID = p1.ID,
                Name = p1.Name,
                Amount = p1.Amount,
                Category = (PL.Category)p1.Category,
                InStock = p1.InStock,
                Price = p1.Price,
            };
            ProductOtemGrid.DataContext = ProductItem;// displays the info to the screeen
        }

        private void addToCart_Click(object sender, RoutedEventArgs e)//butten to add a product item one to the cart
        {
            if (Cart == null) return;//if the cart is null
            try
            {
                if (ProductItem.InStock == true)// if there is enough in stock we can add it  
                {
                    ProductItem.Amount++;//add to the amount 
                    bl?.Cart.AddProductToCart(Cart, ProductItem.ID);//adds it to our cart 
                    action(Cart, ProductItem);
                }
                else
                {
                    MessageBox.Show("Product is out of stock, sorry");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//button to take a single product item out of the cart
        {
            if (Cart == null) return;//if the cart is null
            try
            {
                if (ProductItem.Amount >= 1)// if there is enough in stock we can add it  
                {
                    ProductItem.Amount--;//add to the amount 
                    bl?.Product.UpdateAmountOfProduct(ProductItem.ID, -1);
                    Cart = bl?.Cart.UpdateAmountOfProductInCart(Cart, ProductItem.ID, ProductItem.Amount);//adds it to our cart 
                    action(Cart, ProductItem);
                }
                else
                {
                    MessageBox.Show("Product is not in cart");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
