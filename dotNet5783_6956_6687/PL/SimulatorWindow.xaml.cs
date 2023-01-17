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
        OrderPl orderPl = new OrderPl();
        internal static readonly Random rand = new Random();//to generat random
        public SimulatorWindow()
        {
            InitializeComponent();
            orderForLists = new ObservableCollection<OrderForList>(bl?.Order.GetOrderList().ToList());
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
                MessageBox.Show("Try next time...");
            }
            else
            {
                MessageBox.Show("Mazal Tov!!");
            }
        }

        private void Simulator_bgw_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {

            //orderPl.ID = order.ID;
            //orderPl.DeliveryDate = order.DeliveryDate;
            //orderPl.ShipDate = order.ShipDate;
            //orderPl.OrderDate = order.OrderDate;
            orderPl.Status = (global::OrderStatus)(OrderStatus)Enum.Parse(typeof(OrderStatus), order.Status.ToString());
            //orderPl.CustomerAddress = order.CustomerAddress!;
            //orderPl.CustomerEmail = order.CustomerEmail!;
            //orderPl.CustomerName = order.CustomerName!;
            //orderPl.Items = order.Items;
            //orderPl.TotalPrice = order.TotalPrice;
            //orderPl.wasChanged = order.wasChanged;

            // MessageBox.Show(orderPl.Status.ToString());

            //var item = orderForLists.FirstOrDefault(x => x.ID == order.ID);
            //int index = orderForLists.IndexOf(item);
            //OrderForList ofl1 = new OrderForList() { ID = item.ID, AmountOfItems = item.AmountOfItems, CustomerName = item.CustomerName, Status = (BO.OrderStatus)orderPl.Status, TotalPrice = item.TotalPrice };
            //orderForLists[index] = ofl1;

        }

        private void Simulator_bgw_DoWork(object? sender, DoWorkEventArgs e)
        {
            int presentage = 0;
            while (orderForLists.Any(x => (OrderStatus)x.Status != OrderStatus.delivered))
            {
                if (simulator_bgw.CancellationPending == true)// simulator was stoped 
                {
                    e.Cancel = true;
                    break;
                }
                else  //we want to start changing stuff
                {

                    var allOrders = (from x in orderForLists
                                     group x by x.Status into g
                                     select g).Reverse().ToList();

                    if (allOrders[0].Key == BO.OrderStatus.ordered)
                    {
                        order = bl?.Order.GetOrderInfo(allOrders[0].FirstOrDefault().ID);//the order we want to update now
                        order = bl.Order.UpdateShippingDate(order.ID);//updating the id
                        presentage = rand.Next(25, 50);
                    }

                    else if (allOrders[0].Key == BO.OrderStatus.shipped)
                    {
                        if (order.wasChanged)//we already updated it once
                        {
                            order = bl?.Order.GetOrderInfo(allOrders[0].FirstOrDefault().ID);//the order we want to update now
                            order.Status = BO.OrderStatus.delivered;
                            presentage = 100;
                        }
                        else //this way it stops at some point
                        {
                            order = bl?.Order.GetOrderInfo(allOrders[0].FirstOrDefault().ID);//the order we want to update now
                                                                                             //order = bl.Order.UpdateShippingDate(order.ID);//updating the id
                            presentage = rand.Next(50, 85);
                            order.wasChanged = true;//checks if it was already updated
                        }
                    }
                    //Thread.Sleep(500);
                    if (simulator_bgw.WorkerReportsProgress == true)
                        simulator_bgw.ReportProgress(presentage);
                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (simulator_bgw.IsBusy != true) //if its not in the middle of working
            {
                simulator_bgw.RunWorkerAsync();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (simulator_bgw.WorkerSupportsCancellation == true)//if it supports canclation
            {
                simulator_bgw.CancelAsync();// Cancel the asynchronous operation- cancle the thread
            }
            orderForLists = new ObservableCollection<OrderForList>(bl.Order.GetOrderList());
        }
    }
}
public enum OrderStatus
{
    ordered, shipped, delivered
}
public class OrderPl : DependencyObject
{
    public string CustomerName { get; set; }
    public int ID { get; set; }
    //public Status PersonalStatus { get; set; }


    public OrderStatus Status
    {
        get { return (OrderStatus)GetValue(StatusProperty); }
        set { SetValue(StatusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for PersonalStatus.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StatusProperty =
        DependencyProperty.Register("Status", typeof(OrderStatus), typeof(OrderPl), new PropertyMetadata(OrderStatus.ordered));


    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public double TotalPrice { get; set; }
    public List<BO.OrderItem> Items { get; set; }
    public bool wasChanged { get; set; }

    public override string ToString() => $@"
Order ID: {ID}
Customers name: {CustomerName}
Customers email:{CustomerEmail}
Customers address:{CustomerAddress}
Order date: {OrderDate}
Shipping date: {ShipDate}
Delivery date:{DeliveryDate}
Stasus:{Status}
Items:{string.Join('\n', Items)}
Total price:{TotalPrice}

";
}
