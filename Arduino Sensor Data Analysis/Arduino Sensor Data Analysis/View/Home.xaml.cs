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

        SDA_Core.Data.SensorDataArray sda;
        DataTable procesado, datastructure;
        SDA_Core.Communication.USB usb;
        SDA_Core.Data.Processing pro;

        public Home()
        {
            InitializeComponent();
            IO = new Interface.Home();

            IO.LoadSerialPorts(CB_Ports);
            IO.LoadConnectionModes(CB_ConnectionMode);
            IO.LoadDataStructure(DG_ColumnList);
        }

        private void DG_ColumnList_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void B_NewColumn_Click(object sender, RoutedEventArgs e)
        {
            IO.NewColumnToStructure(TB_ColumnName, TB_ColumnMeasure, DG_ColumnList);
        }

        private void B_DeleteColumn_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteColumnToStructure(TB_ColumnName, TB_ColumnMeasure, DG_ColumnList);
        }

        private void B_Connect_Click(object sender, RoutedEventArgs e)
        {
            IO.StartConnection(CB_Ports, TB_BaudRate, CB_ConnectionMode, DG_ColumnList, DG_Monitor, G_Serial, B_Connect);
        }

        private void B_Cancel_Click(object sender, RoutedEventArgs e)
        {
            IO.CloseConnections(G_Serial, B_Connect);
        }


    }
}
