using BlApi;
using BO;
using Microsoft.VisualBasic;
using PL.PlProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CartWindow1.xaml
    /// </summary>
    public partial class CartWindow1 : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        private ObservableCollection<OrderItem> OrderItemList { get; set; }//for the listview
        private BO.Cart cart1 = new BO.Cart();
        private PL.PlProduct.Cart cart2 = new PL.PlProduct.Cart();
        Action<BO.Cart, Products>? action;
        public CartWindow1()
        {
            InitializeComponent();
        }
        public CartWindow1(BO.Cart cart, Action<BO.Cart, Products>? action) :this()
        {
            this.action = action;
            cart1 = cart;
            cart2.TotalPrice = cart.TotalPrice;
            if (cart.Items != null) 
            {
                OrderItemList = new ObservableCollection<OrderItem>(cart.Items);
                Products_DataGrid.DataContext = OrderItemList;
                this.DataContext = cart2;
            }
            else
            {
                OrderItemList = new ObservableCollection<OrderItem>();
            }
        }
        private void ProceedToCheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cart1.Items == null || cart1.Items.Count == 0)
                {
                    MessageBox.Show("Cart is empty, Please add product befor you check out");
                }
                else//if the cart has stuff in it
                {
                    CustomerInfoWindow ciw = new CustomerInfoWindow(cart1);
                    ciw.ShowDialog();//opens the window that costomer can put info in
                }
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //try
            //{
            //    cart1.CustomerEmail =cu

            //    bl.Cart.confirmCart(cart1,)
            //   // BO.Cart cart = new BO.Cart()
                

            //}
            //catch(Exception ex) { MessageBox.Show(ex.ToString()); }//if theres a problem           
        }

        private void ContinueShoppingButton_Click(object sender, RoutedEventArgs e)
        { 
            this.Close();
        }

        private void RemoveProductFromCartButton_Click(object sender, RoutedEventArgs e)//remove from cart
        {
            try
            {
                OrderItem orderItem = Products_DataGrid.SelectedItem as OrderItem;//orderitem is the product they want to remove from cart
                if (orderItem != null)
                {
                    cart1 = bl?.Cart.UpdateAmountOfProductInCart(cart1, orderItem.ProductID, 0);//takes the product out of the cart
                    OrderItemList = new ObservableCollection<OrderItem>(cart1.Items);
                    Products_DataGrid.DataContext = OrderItemList;
                    cart2.TotalPrice = cart1.TotalPrice;//updates the total price of the cart
                    Products productItem = new Products()
                    {
                        ID = orderItem.ProductID,
                        Amount = 0,
                    };
                    action(cart1, productItem);
                }
            }
            catch(Exception ex)//if theres a problem
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeAmountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderItem orderItem = Products_DataGrid.SelectedItem as OrderItem;//orderitem is the product they want to update amount in cart
                if (orderItem != null)
            {
                    int amount;
                    int.TryParse(Interaction.InputBox("Please enter new amount", "Chang product amount in cart", ""), out amount);//displays an inputbox and gets the id
                    if (amount > 0)//making sure there is text
                    {
                        cart1 = bl?.Cart.UpdateAmountOfProductInCart(cart1, orderItem.ProductID, amount);//updates the amount of the product in the cart
                        OrderItemList = new ObservableCollection<OrderItem>(cart1.Items);
                        Products_DataGrid.DataContext = OrderItemList;
                        cart2.TotalPrice = cart1.TotalPrice;
                        Products productItem = new Products()
                        {
                            ID = orderItem.ProductID,
                            Amount = amount,
                        };
                        action(cart1, productItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
