using BlApi;
using BO;
using Microsoft.VisualBasic;
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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new BlImplementation.Bl();

        public ProductWindow(ProductForList? myData)
        {
            InitializeComponent();
            Category_ComboBox.ItemsSource = BO.Category.GetValues(typeof(BO.Category));//combobox source of info- categories
            this.myData = myData;
            if (myData != null)//if came with info - from product list window
            {
                Id_Textbox.Text = myData.ID.ToString();
                Name_Textbox.Text = myData.Name;
                Price_Textbox.Text = myData.Price.ToString();
                InStock_Textbox.Text = myData.Amount.ToString();
                Category_ComboBox.Text= myData.Category.ToString();
                AddProduct_Button.Visibility = Visibility.Hidden;
            }
            else
            {
                UpdateProduct_button.Visibility = Visibility.Hidden;
            }
        }

        public ProductWindow()
        {
            InitializeComponent();
            Category_ComboBox.ItemsSource = BO.Category.GetValues(typeof(BO.Category));//combobox source of info- categories
        }
        public BO.ProductForList myData { get; set; }

        private void AddProduct_Button_Click(object sender, RoutedEventArgs e)
        {//adds a product to the do
         //if (Id_Textbox.Text == null || Category_ComboBox.SelectedItem == null || Price_Textbox.Text == null || InStock_Textbox.Text == null || Name_Textbox.Text == null)//if there is missing data

            try
            {
                BO.Product product = new BO.Product()
                {
                    Id = int.Parse(Id_Textbox.Text),
                    Name = Name_Textbox.Text,
                    Price = double.Parse(Price_Textbox.Text),
                    InStock = int.Parse(InStock_Textbox.Text),
                    Category = (BO.Category)Category_ComboBox.SelectedItem

                };
                try
                {
                    bl.Product.AddProduct(product);//adds the product to the do
                    MessageBox.Show("product added successfully");
                    this.Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)//if missing any data
            {
                MessageBox.Show("Please add missing data");
            }

        }


        private void Category_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpdateProduct_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
