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
        SDA_Core.Communication.SerialConnection srl = new SDA_Core.Communication.SerialConnection(new SDA_Core.Communication.SerialConfiguration("COM4", 9600));
        SDA_Core.Data.Containers.SensorData data = new SDA_Core.Data.Containers.SensorData(2);
        List<SDA_Core.Data.Containers.Measurement> formatList = new List<SDA_Core.Data.Containers.Measurement>();
        DataTable res = null;
        Thread _thread;
        
        public Home()
        {
            InitializeComponent();
            IO = new Interface.Home();
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
                //DG_Monitor.DataContext = res;
                DG_Monitor.Items.Refresh();
                await Task.Delay(500);
            }
        }

        private async void leer()
        {
            Debug.WriteLine("Funcionando!!");
            bool first = true;
            while (srl.Available())
            {
                pr.Process(srl.Read(), ref data, formatList);
                for (int i = 0; i < data.Rows; ++i)
                {
                    for (int j = 0; j < data.Columns; ++j)
                    {
                        Debug.WriteLine(data[i][j].Value + " " + data[i][j].Measure);
                    }
                }
                if (res == null) res = data.ToDataTable();
                else
                {
                    res = data.ToDataTable();
                    //data.UpdateDataTable(res);
                }
              //  DG_Monitor.DataContext = res;
                
               /*  if (first && data.Size > 0)
                 {
                     first = false;
                     res = data.ToDataTable();
                     DG_Monitor.DataContext = res;
                 }
                 if (!first)
                 {
                     data.UpdateDataTable(res);
                     DG_Monitor.DataContext = res;
                 }*/
                await Task.Delay(500);
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
