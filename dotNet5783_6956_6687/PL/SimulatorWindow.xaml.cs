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
        OrderPl orderPl = new OrderPl();
        internal static readonly Random rand = new Random();//to generat random
        public SimulatorWindow()
        {
            InitializeComponent();
            // var orderForLists1 = (from x in bl?.Order.GetOrderList()
            //                    select new OrderForListPl() { ID = x.ID, AmountOfItems = x.AmountOfItems, CustomerName = x.CustomerName, TotalPrice = x.TotalPrice, Status = (BO.OrderStatus)order.Status });
            orderForLists = new ObservableCollection<OrderForList>(bl?.Order.GetOrderList());
            simulator_bgw = new BackgroundWorker();
            simulator_bgw.DoWork += Simulator_bgw_DoWork;
            simulator_bgw.ProgressChanged += Simulator_bgw_ProgressChanged;
            simulator_bgw.RunWorkerCompleted += Simulator_bgw_RunWorkerCompleted;
            simulator_bgw.WorkerSupportsCancellation = true;
            simulator_bgw.WorkerReportsProgress = true;
            this.DataContext = orderForLists;
        }



        private void Simulator_bgw_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
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

        private void Simulator_bgw_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            //orderPl.ID = order.ID;
            //orderPl.DeliveryDate = order.DeliveryDate;
            //orderPl.ShipDate = order.ShipDate;
            //orderPl.OrderDate = order.OrderDate;
            //orderPl.Status = (BO.OrderStatus)order.Status;
            //orderPl.CustomerAddress = order.CustomerAddress!;
            //orderPl.CustomerEmail = order.CustomerEmail!;
            //orderPl.CustomerName = order.CustomerName!;
            //orderPl.Items = order.Items;
            //orderPl.TotalPrice = order.TotalPrice;
            //orderPl.wasChanged = order.wasChanged;
            // MessageBox.Show(orderPl.Status.ToString());
            //  var allOrders = bl?.Order.GetAllOrders().OrderBy(x => x?.OrderDate).ToList();
         
            var item = orderForLists.FirstOrDefault(x => x.ID == e.ProgressPercentage);
            int index = orderForLists.IndexOf(item);
            OrderForList ofl1 = new OrderForList() { ID = item.ID, AmountOfItems = item.AmountOfItems, CustomerName = item.CustomerName, Status = item.Status + 1, TotalPrice = item.TotalPrice };
            orderForLists[index] = ofl1;
            orderForLists = new ObservableCollection<OrderForList>(bl?.Order.GetOrderList());
            this.DataContext = orderForLists;

            Thread.Sleep(1000);
            // Thread.Sleep(5000);
        }
        private void Simulator_bgw_DoWork(object? sender, DoWorkEventArgs e)
        {
            // int presentage = 0;
            while (orderForLists.Any(x => (OrderStatus)x.Status != OrderStatus.delivered))
            {
                Thread.Sleep(2000);
                
                if (simulator_bgw.CancellationPending == true) // simulator was stoped 
                {
                    e.Cancel = true;
                    break;
                }
                else  //we want to start changing stuff
                {
                   
                    int? id = bl?.Order.LastTouched();
                    if (id == null)
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
                            time = time.AddDays(1);
                            order = bl?.Order.UpdateShippingDate(order.ID, time);
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                        int random = rand.Next(3, 10);
                        Thread.Sleep(random *1000);
                        if (simulator_bgw.WorkerReportsProgress == true)
                            simulator_bgw.ReportProgress(order.ID);//we are sending the status and presentage
                                                                   // presentage = rand.Next(25, 50);
                    }
                    else if (order?.Status == BO.OrderStatus.shipped)//shipped
                    {
                        order.Status = BO.OrderStatus.delivered;
                        time = time.AddDays(1);
                        order = bl?.Order.UpdateDeliveryDate(order.ID, time);
                        //presentage = 100;
                        int random = rand.Next(3, 10);
                        Thread.Sleep(random * 1000);
                        if (simulator_bgw.WorkerReportsProgress == true)
                            simulator_bgw.ReportProgress(order.ID);//we are sending the status and presentage
                                                                   //  }
                                                                   //else //this way it stops at some point
                                                                   //  {
                                                                   // order = bl?.Order.GetOrderInfo(allOrders[0].FirstOrDefault().ID);//the order we want to update now
                                                                   //order = bl.Order.UpdateShippingDate(order.ID);//updating the id
                                                                   // presentage = rand.Next(50, 85);
                                                                   // order.wasChanged = true;//checks if it was already updated
                                                                   //    continue;

                    }

                    //if (allOrders[0].Key == (BO.OrderStatus)OrderStatus.ordered)
                    //{
                    //    order = bl?.Order.GetOrderInfo(allOrders[0].FirstOrDefault().ID);//the order we want to update now
                    //    try { order = bl?.Order.UpdateShippingDate(order.ID); }//updating the id}
                    //       catch (Exception ex) { MessageBox.Show(ex.Message); }
                    //    presentage = rand.Next(25, 50);
                    //}
                    //else if (allOrders[0].Key == (BO.OrderStatus)OrderStatus.shipped)
                    //{
                    //    if (order.wasChanged)//we already updated it once
                    //    {
                    //        order = bl?.Order.GetOrderInfo(allOrders[0].FirstOrDefault().ID);//the order we want to update now
                    //        order.Status = BO.OrderStatus.delivered;
                    //        presentage = 100;
                    //    }
                    //    else //this way it stops at some point
                    //    {
                    //        order = bl?.Order.GetOrderInfo(allOrders[0].FirstOrDefault().ID);//the order we want to update now
                    //                                                                         //order = bl.Order.UpdateShippingDate(order.ID);//updating the id
                    //        presentage = rand.Next(50, 85);
                    //        order.wasChanged = true;//checks if it was already updated
                    //    }
                }

            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (simulator_bgw.IsBusy != true) //if its not in the middle of working
            {
               // MessageBox.Show("starting");
                simulator_bgw.RunWorkerAsync();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (simulator_bgw.WorkerSupportsCancellation == true)//if it supports canclation
            {
                simulator_bgw.CancelAsync();// Cancel the asynchronous operation- cancle the thread
            }
            
        }

        private void infoButton_Click(object sender, RoutedEventArgs e)
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
            BO.OrderStatus Status = (BO.OrderStatus)Enum.Parse(typeof(BO.OrderStatus), value.ToString()!);// ; (Category)Enum.Parse(typeof(Category), Category)

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
public class OrderPl : INotifyPropertyChanged
{
    public string CustomerName { get; set; }
    public int ID { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public double TotalPrice { get; set; }
    public List<BO.OrderItem> Items { get; set; }
    public bool wasChanged { get; set; }
    private OrderStatus _status;
    public OrderStatus Status
    {
        get { return _status; }
        set
        {
            _status = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //    public override string ToString() => $@"
    //Order ID: {ID}
    //Customers name: {CustomerName}
    //Customers email:{CustomerEmail}
    //Customers address:{CustomerAddress}
    //Order date: {OrderDate}
    //Shipping date: {ShipDate}
    //Delivery date:{DeliveryDate}
    //Stasus:{Status}
    //Items:{string.Join('\n', Items)}
    //Total price:{TotalPrice}

    //";
}
public class OrderForListPl : INotifyPropertyChanged
{
    public int ID { get; set; }
    public string CustomerName { get; set; }

    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }

    private OrderStatus _status;
    public OrderStatus Status
    {
        get { return _status; }
        set
        {
            _status = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
