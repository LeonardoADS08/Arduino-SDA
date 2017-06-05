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
        SDA_Core.Data.Processing pr = new SDA_Core.Data.Processing();
        SDA_Core.Communication.SerialConnection srl = new SDA_Core.Communication.SerialConnection("COM4", new SDA_Core.Communication.BaudRates(9600));
        SDA_Core.Data.Containers.SensorData data = new SDA_Core.Data.Containers.SensorData(2);
        List<SDA_Core.Data.Containers.Measurement> formatList = new List<SDA_Core.Data.Containers.Measurement>();
        DataTable res = null;
        Thread _thread;
        
        public Home()
        {
            InitializeComponent();
            IO = new Interface.Home();

            IO.LoadSerialPorts(CB_Ports);
            IO.LoadBaudRates(CB_BaudRate);
            IO.LoadSensors(CB_Sensors);

            SDA_Core.Data.Containers.Measurement ms1 = new SDA_Core.Data.Containers.Measurement("Time", "ms.");
            SDA_Core.Data.Containers.Measurement ms2 = new SDA_Core.Data.Containers.Measurement("Light", "int.");
            formatList.Add(ms1);
            formatList.Add(ms2);

            _thread = new Thread(() => leer());

            srl.Open();
            _thread.Start();
            update();
            
            //leer();

        }

        private async void update()
        {
            while (srl.Available())
            {
                DG_Monitor.DataContext = SDA_Core.Program.SerialMonitor;
                await Task.Delay(100);
            }
        }

        private async void leer()
        {
            while (srl.Available())
            {
                pr.Process(srl.Read(), ref data, formatList);
                SDA_Core.Program.SerialMonitor = data.ToDataTable();
                await Task.Delay(100);
            }
        }

        private void DG_ColumnList_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void B_NewColumn_Click(object sender, RoutedEventArgs e)
        {
            //  IO.NewColumnToStructure(TB_ColumnName, TB_ColumnMeasure, DG_ColumnList);
        }

        private void B_DeleteColumn_Click(object sender, RoutedEventArgs e)
        {
           // IO.DeleteColumnToStructure(TB_ColumnName, TB_ColumnMeasure, DG_ColumnList);
        }

        private void B_Connect_Click(object sender, RoutedEventArgs e)
        {
         //   IO.StartConnection(CB_Ports, TB_BaudRate, CB_ConnectionMode, DG_ColumnList, DG_Monitor, G_Serial, B_Connect);
        }

        private void DG_Monitor_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //label.Content = DG_Monitor.Items.Count.ToString();
        }

        private void B_Cancel_Click(object sender, RoutedEventArgs e)
        {
          //  IO.CloseConnections(G_Serial, B_Connect);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SDA_Program.View.Popups.Sensors temp = new Popups.Sensors();
            temp.Show();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            SDA_Program.View.Popups.BaudRates temp = new Popups.BaudRates();
            temp.Show();
        }

        private void I_RefreshPorts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IO.LoadSerialPorts(CB_Ports);
        }

        private void I_RefreshBaudRates_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IO.LoadBaudRates(CB_BaudRate);
        }
        private void I_RefreshSensors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IO.LoadSensors(CB_Sensors);
        }

    }
}
