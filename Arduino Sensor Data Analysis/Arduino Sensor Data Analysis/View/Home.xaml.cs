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
        SDA_Core.Communication.SerialConnection srl = new SDA_Core.Communication.SerialConnection(new SDA_Core.Communication.SerialConfiguration("COM4", 9600));

        public Home()
        {
            InitializeComponent();
            IO = new Interface.Home();

            srl.Open();
            leer();
            SDA_Core.Program._data.Headers.MeasureList.Measures.Add(new SDA_Core.Data.Containers.Measurement("Time", "ms."));
            SDA_Core.Program._data.Headers.MeasureList.Measures.Add(new SDA_Core.Data.Containers.Measurement("Light", "int."));
            // IO.LoadSerialPorts(CB_Ports);
            /*IO.LoadConnectionModes(CB_ConnectionMode);
            IO.LoadDataStructure(DG_ColumnList);
            */
        }

        private async void leer()
        {
            while (srl.Available())
            {
                pr.Process(srl.Read(), ref SDA_Core.Program._data);
                DG_Monitor.DataContext = SDA_Core.Program._data.ToDataTable();
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


    }
}
