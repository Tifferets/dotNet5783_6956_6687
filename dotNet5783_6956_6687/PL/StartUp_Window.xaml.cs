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
    /// Interaction logic for StartUp_Window.xaml
    /// </summary>
    public partial class StartUp_Window : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();

        public StartUp_Window()
        {
            InitializeComponent();
        }
        private void Button_Click_Admin(object sender, RoutedEventArgs e) => new Admin_Window().Show();

        private void Button_Click_NewOrder(object sender, RoutedEventArgs e) => new NewOrderWindow().Show();

        private void Button_Click_TrackOrder(object sender, RoutedEventArgs e)=> new TrackOrder_Window().Show();

        private void Next_button_Click(object sender, RoutedEventArgs e)
        {
            if (Admin.IsChecked == true)
            {
                new Admin_Window().Show();
            }
            else if (NewOrder.IsChecked == true)
            {
                new NewOrderWindow().Show();
            }
            else if (TrackOrder.IsChecked == true)
            {
                new TrackOrder_Window().Show();
            }
        }
    }
}
