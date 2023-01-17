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
        public SimulatorWindow()
        {
            InitializeComponent();
            simulator_bgw = new BackgroundWorker();
            simulator_bgw.DoWork += Simulator_bgw_DoWork;
            simulator_bgw.ProgressChanged += Simulator_bgw_ProgressChanged;
            simulator_bgw.RunWorkerCompleted += Simulator_bgw_RunWorkerCompleted;

            simulator_bgw.WorkerSupportsCancellation= true;
            simulator_bgw.WorkerReportsProgress= true;
        }

       

        private void Simulator_bgw_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Simulator_bgw_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Simulator_bgw_DoWork(object? sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//start the simulator button
        {
            if(simulator_bgw.IsBusy != true) //if its not in the middle of working
            {
                simulator_bgw.RunWorkerAsync();//we need to send the length of our loop- how long it will take 
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//cancle the simulator button
        {
            if (simulator_bgw.WorkerSupportsCancellation == true)//if it supports canclation
            {
                simulator_bgw.CancelAsync();// Cancel the asynchronous operation- cancle the thread
            }
            orderForLists = new ObservableCollection<OrderForList>(bl.Order.GetOrderList());

        }
    }
}
