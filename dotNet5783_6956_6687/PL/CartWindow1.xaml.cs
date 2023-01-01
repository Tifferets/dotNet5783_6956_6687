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
    /// Interaction logic for CartWindow1.xaml
    /// </summary>
    public partial class CartWindow1 : Window
    {
        public CartWindow1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.Cart cart = new BO.Cart();
            //  cart =
            MessageBox.Show("Thank you and have a nice day");
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)=> this.Close();

    }
}
