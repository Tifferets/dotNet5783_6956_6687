using BlApi;
using BO;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
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
        private BlApi.IBl? bl = BlApi.Factory.Get();

        public ProductWindow(ProductForList? myData)//gets value from selecting on te comboBox to update
        {
            InitializeComponent();
            Category_ComboBox.ItemsSource = BO.Category.GetValues(typeof(BO.Category));//combobox source of info- categories
            this.myData = myData;
            if (myData != null)//if came with info - from product list window
            {
                Id_Textbox.Text = myData.ID.ToString();
                Id_Textbox.IsEnabled = false;// no accesses to change the id
                Name_Textbox.Text = myData.Name;
                Price_Textbox.Text = myData.Price.ToString();
                InStock_Textbox.Text = myData.Amount.ToString();
                Category_ComboBox.Text= myData.Category.ToString();
                AddProduct_Button.Visibility = Visibility.Hidden;//add butten invisable
            }
        }

        public ProductWindow()
        {
           InitializeComponent();
            Category_ComboBox.ItemsSource = BO.Category.GetValues(typeof(BO.Category));//combobox source of info- categories
            UpdateProduct_button.Visibility = Visibility.Hidden;//update butten invisable

        }
        public BO.ProductForList? myData { get; set; }

        private void AddProduct_Button_Click(object sender, RoutedEventArgs e)//adds a product to the DO list
        {
            try
            {
                if(Category_ComboBox.SelectedItem == null) //if they didnt enter a category 
                {
                    MessageBox.Show("please select a category");
                    return;
                }
                BO.Product product = new BO.Product()//creating a new product
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
                MessageBox.Show("Please enter missing data");
            }

        }

        private void UpdateProduct_button_Click(object sender, RoutedEventArgs e)//to pudate a product
        {
            try
            {
                if (Name_Textbox.Text == "")//if there isnt a name entered
                {
                    MessageBox.Show("Please enter a name");
                    return;
                }
                BO.Product product = new BO.Product()//crating a new product
                {
                    Id = int.Parse(Id_Textbox.Text),
                    Name = Name_Textbox.Text,
                    Price = double.Parse(Price_Textbox.Text),
                    InStock = int.Parse(InStock_Textbox.Text),
                    Category = (BO.Category)Category_ComboBox.SelectedItem
                };
                try
                {
                    bl.Product.UpdateProduct(product);//adds the product to the do
                    MessageBox.Show("product updated successfully");
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)//if missing any data
            {
                MessageBox.Show("Please enter missing data");
            }
        }
        private void PreviewTextImputString(object sender, TextCompositionEventArgs e)// for name -only lets to put letters 
        {
            e.Handled = IsTextAllowedString(e.Text);//checks what is there
        }
        private static readonly Regex regex_str = new Regex("[^A-Z a-z]+");//only lets it be a letter
        private static bool IsTextAllowedString(string text) //for name - makes sure the imput is a letter
        {
            return regex_str.IsMatch(text);
        }

        private void PreviewTextImput(object sender, TextCompositionEventArgs e)// for price- only lets to put numbers, can with decimal 
        {
            e.Handled= !IsTextAllowed(e.Text);
        }
        private static readonly Regex _regex = new Regex("[^0-9.]+"); //regex that matches disallowed text -only positive and dec
        private static bool IsTextAllowed(string text) //for price
        {
            return !_regex.IsMatch(text);
        }

        private void PreviewTextImputNoDec(object sender, TextCompositionEventArgs e)// for InStock and id- only lets to put numbers, without decimal 
        {
            e.Handled = !IsTextAllowedDec(e.Text);
        }
        private static readonly Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text-only positive no decimal
        private static bool IsTextAllowedDec(string text)// for in stock and id
        {
            return !regex.IsMatch(text);
        }


    }
}
