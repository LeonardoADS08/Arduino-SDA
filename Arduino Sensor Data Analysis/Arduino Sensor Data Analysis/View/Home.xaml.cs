using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

namespace SDA_Program.View
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Interface.HomeInterface IO;
        private Application.HomeApplication APP;

        public Home()
        {
            InitializeComponent();
            IO = new Interface.HomeInterface();
            APP = new Application.HomeApplication();

            IO.LoadSerialPorts(CB_Ports);
            IO.LoadBaudRates(CB_BaudRate, APP.GetBaudRates());
            IO.LoadSensors(CB_Sensors, APP.GetSensors());

            IO.CheckConnection(G_SerialConnection, G_ReceivedData, B_Connect);
        }

        private void I_RefreshPorts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IO.LoadSerialPorts(CB_Ports);
        }

        private void I_RefreshBaudRates_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IO.LoadBaudRates(CB_BaudRate, APP.GetBaudRates());
        }

        private void B_Delete_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteSensor(DG_SensorsList);
        }

        private void B_Add_Click(object sender, RoutedEventArgs e)
        {
            IO.AddSensor(DG_SensorsList, CB_Sensors);
        }

        private void I_RefreshSensors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IO.LoadSensors(CB_Sensors, APP.GetSensors());
        }

        private void B_Connect_Click(object sender, RoutedEventArgs e)
        {
            IO.StartConnection(DG_Monitor, CB_Ports, CB_BaudRate, G_SerialConnection, G_ReceivedData, B_Connect);
        }

        private void B_Cancel_Click(object sender, RoutedEventArgs e)
        {
            IO.StopConnection(G_SerialConnection, G_ReceivedData, B_Connect);
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