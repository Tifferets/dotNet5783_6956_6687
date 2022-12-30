using BO;
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
    /// Interaction logic for TrackOrder_Window.xaml
    /// </summary>
    public partial class TrackOrder_Window : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<OrderTracking> orderTrackings { get; set; }
        private int id;
        public TrackOrder_Window()
        {
            InitializeComponent();
            var lst = from OrderForList? item in bl?.Order.GetOrderList()
                      select (bl.Order.OrderStatus(item.ID));

            orderTrackings = new ObservableCollection<OrderTracking>(lst);
            //TrackOrder_Grid.DataContext = bl?.Order.OrderStatus(100000);
            //TrackOrder_Grid.DataContext = orderTrackings;
        }

        private void Id_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if(Id_textbox.Text.Length == 6)
            //{
            //    Details_dataGrid.DataContext = orderTrackings;
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Id_textbox.Text.Length == 6)
            {
                int.TryParse(Id_textbox.Text, out id);
                TrackOrder_Grid.DataContext = bl?.Order.OrderStatus(id);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try 
            {
                Order? order = bl?.Order.GetOrderInfo(id);
                OrderWindow orderWindow = new OrderWindow(order);
                orderWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
/*
 <ListBox ItemsSource="{Binding tracking}" Grid.Column="1" Grid.Row="1"  HorizontalContentAlignment="Stretch">
                <ListBox.ItemTemplate>
                    <DataTemplate>
                        <Grid>
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition/>
                                <ColumnDefinition/>
                                <ColumnDefinition/>
                            </Grid.ColumnDefinitions>
                            <TextBlock Grid.Column="0" Text="{Binding Item1}"/>
                            <TextBlock Grid.Column="1" Text="{Binding Item2}"/>
                            <TextBlock Grid.Column="2" Text="{Binding Item3}"/>
                        </Grid>
                    </DataTemplate>
                </ListBox.ItemTemplate>
            </ListBox>

<DataGridTemplateColumn x:Name="OrderDateColumn" Header="Order Date" Width="SizeToHeader">
                <DataGridTemplateColumn.CellTemplate>
                    <DataTemplate>
                        <DatePicker SelectedDate="{Binding }"/>
                    </DataTemplate>
                </DataGridTemplateColumn.CellTemplate>
            </DataGridTemplateColumn>
            <DataGridTemplateColumn x:Name="ShippingDateColumn" Header="Shipping Date" Width="SizeToHeader">
                <DataGridTemplateColumn.CellTemplate>
                    <DataTemplate>
                        <DatePicker SelectedDate="{Binding }"/>
                    </DataTemplate>
                </DataGridTemplateColumn.CellTemplate>
            </DataGridTemplateColumn>
*/
