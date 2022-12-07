using BlApi;
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
        public BO.ProductForList? myData { get; set; }  
        public ProductWindow()
        {
            InitializeComponent();
            Category_ComboBox.ItemsSource = BO.Category.GetValues(typeof(BO.Category));//combobox source of info- categories
            if(myData !=null)//if came with info
            {
                Id_Textbox.Text =myData.ID.ToString();
                Name_Textbox.Text = myData.Name;
                Price_Textbox.Text =myData.Price.ToString();
                InStock_Textbox.Text =myData.InStock.ToString();
                Category_ComboBox.SelectedItem =myData.Category;
            }
        }


        private void AddProduct_Button_Click(object sender, RoutedEventArgs e)
        {//adds a product to the do
            BO.Product product = new BO.Product()
            {
                    Id = int.Parse( Id_Textbox.Text),
                    Name = Name_Textbox.Text,
                    Price = double.Parse(Price_Textbox.Text),
                    InStock = int.Parse(InStock_Textbox.Text),
                    Category = (BO.Category)Category_ComboBox.SelectedItem

            };
            bl.Product.AddProduct(product);//adds the product to the do
        }

        private void Category_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpdateProduct_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
