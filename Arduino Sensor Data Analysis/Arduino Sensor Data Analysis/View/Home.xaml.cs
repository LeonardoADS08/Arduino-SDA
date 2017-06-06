using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Diagnostics;

namespace SDA_Program.View
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        Interface.Home IO;
        
        public Home()
        {
            InitializeComponent();
            IO = new Interface.Home();

            IO.LoadSerialPorts(CB_Ports);
            IO.LoadBaudRates(CB_BaudRate);
            IO.LoadSensors(CB_Sensors);

            IO.LoadReceivedSensors(DG_SensorsList);

            IO.CheckLists(CB_Ports, CB_BaudRate, CB_Sensors);
        }

        private async void UpdateSerialMonitor()
        {
            while (IO.SerialConnection.Available())
            {
                DG_Monitor.DataContext = SDA_Core.Program.SerialMonitor;
                await Task.Delay(100);
            }
            G_Serial.IsEnabled = true;
        }

        private void B_Connect_Click(object sender, RoutedEventArgs e)
        {
            IO.StartConnection(CB_Ports, CB_BaudRate);
            UpdateSerialMonitor();
            G_Serial.IsEnabled = false;
        }

        private void B_Cancel_Click(object sender, RoutedEventArgs e)
        {
            IO.StopConnection();
            G_Serial.IsEnabled = true;
        }

        private void I_RefreshPorts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IO.LoadSerialPorts(CB_Ports);
        }

        private void B_BaudRatesLists_Click(object sender, RoutedEventArgs e)
        {
            View.Popups.BaudRates popup = new Popups.BaudRates();
            popup.Show();
        }

        private void B_Delete_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteReceivedSensor(DG_SensorsList);
        }

        private void B_Add_Click(object sender, RoutedEventArgs e)
        {
            IO.SaveReceivedSensor(DG_SensorsList, CB_Sensors);
        }

        private void B_SensorList_Click(object sender, RoutedEventArgs e)
        {
            View.Popups.Sensors popup = new Popups.Sensors();
            popup.Show();
        }

        private void DG_SensorsList_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void DG_Monitor_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
