using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;
using System.Timers;
using System.Globalization;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        BackgroundWorker simulator_bgw;
        private ObservableCollection<OrderForList> orderForLists { get; set; }
        private BlApi.IBl? bl = BlApi.Factory.Get();
        Order? order = new();
        DateTime time = DateTime.Now;
       // OrderPl orderPl = new OrderPl();
        internal static readonly Random rand = new Random();//to generat random
        public SimulatorWindow()
        {
            InitializeComponent();
            orderForLists = new ObservableCollection<OrderForList>(bl?.Order.GetOrderList());
            simulator_bgw = new BackgroundWorker();
            simulator_bgw.DoWork += Simulator_bgw_DoWork;//signing up to all events 
            simulator_bgw.ProgressChanged += Simulator_bgw_ProgressChanged;
            simulator_bgw.RunWorkerCompleted += Simulator_bgw_RunWorkerCompleted;
            simulator_bgw.WorkerSupportsCancellation = true;
            simulator_bgw.WorkerReportsProgress = true;
            this.DataContext = orderForLists;

        }
        private void Simulator_bgw_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)//when we finish the thread
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show("Finished for today");
            }
            else
            {
                MessageBox.Show("All orders delivered");
            }
        }

        private void Simulator_bgw_ProgressChanged(object? sender, ProgressChangedEventArgs e)//chenges the status 
        {
            try
            {
                var item = orderForLists.FirstOrDefault(x => x.ID == e.ProgressPercentage);
                int index = orderForLists.IndexOf(item);
                OrderForList ofl1 = new OrderForList() { ID = item.ID, AmountOfItems = item.AmountOfItems, CustomerName = item.CustomerName, Status = item.Status + 1, TotalPrice = item.TotalPrice };
                orderForLists[index] = ofl1;
                orderForLists = new ObservableCollection<OrderForList>(bl?.Order.GetOrderList());//reloading the list
                this.DataContext = orderForLists;
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (simulator_bgw.WorkerSupportsCancellation == true)
                {
                    simulator_bgw.CancelAsync();// Cancel the asynchronous operation- cancle the thread
                }
            }
        }
        private void Simulator_bgw_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (orderForLists.Any(x => (OrderStatus)x.Status != OrderStatus.delivered))
            {
                Thread.Sleep(2000);
                orderForLists = new ObservableCollection<OrderForList>(bl?.Order.GetOrderList());
                if (simulator_bgw.CancellationPending == true) // simulator was stoped 
                {
                    e.Cancel = true;
                    break;
                }
                else  //we want to start changing stuff
                {

                    int? id = bl?.Order.LastTouched();//gets the order that has been edited the last
                    if (id == null)//if they have all been deliverd we wait to see if another came in
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    else
                    {
                        order = bl?.Order.GetOrderInfo((int)id);//the order we want to update now
                    }
                    if (order?.Status == BO.OrderStatus.ordered)//status is orderd
                    {
                        try
                        {
                            time = time.AddHours(5);//adding time
                            if (time.Day - order.OrderDate?.Day >= 1)//it take 1 day to ship out
                                order = bl?.Order.UpdateShippingDate(order.ID, time);//updateing to the background time
                                                                                     //not to now because we want to see real changes in the date and not just all today
                            else
                                continue;
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                        int random = rand.Next(3, 10);//generting time to sllep
                        Thread.Sleep(random * 1000);
                        if (simulator_bgw.WorkerReportsProgress == true)
                            simulator_bgw.ReportProgress(order.ID);//we are sending the id we just changed
                                                                  
                    }
                    else if (order?.Status == BO.OrderStatus.shipped)//shipped
                    {
                        order.Status = BO.OrderStatus.delivered;
                        time = time.AddDays(1);
                        if (time.Day - order.ShipDate?.Day >= 3)//it takes 3 days to deliver
                            order = bl?.Order.UpdateDeliveryDate(order.ID, time);
                        else continue;
                        
                        int random = rand.Next(3, 10);
                        Thread.Sleep(random * 1000);
                        if (simulator_bgw.WorkerReportsProgress == true)
                            simulator_bgw.ReportProgress(order.ID);//we are sending the id
                    }
                }

            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)//start sim
        {
            Console.Beep();
            if (simulator_bgw.IsBusy != true) //if its not in the middle of working
            {
                simulator_bgw.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("You Cant Run Twice");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)// it supports canclation
        {
            Console.Beep();
            if (simulator_bgw.WorkerSupportsCancellation == true)
            {
                simulator_bgw.CancelAsync();// Cancel the asynchronous operation- cancle the thread
            }

        }

        private void infoButton_Click(object sender, RoutedEventArgs e)//button that displays tracking info 
        {
            OrderForList orderFor = (OrderForList)Orders_DataGrid.SelectedItem;
            if (orderFor != null)
            {
                OrderTracking? orderTracking = bl?.Order.OrderStatus(orderFor.ID);
                MessageBox.Show(orderTracking.ToString());
            }

        }
    }
    public class StatusToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BO.OrderStatus Status = (BO.OrderStatus)Enum.Parse(typeof(BO.OrderStatus), value.ToString()!);
            if (Status == BO.OrderStatus.ordered)
            {
                return Brushes.DeepSkyBlue;
            }
            if (Status == BO.OrderStatus.shipped)
            {
                return Brushes.GreenYellow;
            }
            else
            {
                return Brushes.DeepPink;
            }

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

