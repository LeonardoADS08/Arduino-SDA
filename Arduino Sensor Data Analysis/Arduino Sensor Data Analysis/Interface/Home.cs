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

namespace SDA_Program.Interface
{
    class Home : SDA_Core.Program
    {
        private DataTable _columnStructure, _serialMonitor;

        public Home()
        {
            _columnStructure = new DataTable();
            _columnStructure.Columns.Add("Column", typeof(string));
            _columnStructure.Columns.Add("Measure", typeof(string));

            _serialMonitor = new DataTable();
        }

        /// <summary>
        /// ES: Método para cargar los puertos serial disponibles en un ComboBox.
        /// </summary>
        public void LoadSerialPorts(ComboBox CB_Ports)
        {
            SDA_Core.Communication.SerialConnection serial = new SDA_Core.Communication.SerialConnection();
            List<string> AvailableSerialPorts = serial.SerialPorts();
            for (int i = 0; i < AvailableSerialPorts.Count; ++i)
            {
                CB_Ports.Items.Add(AvailableSerialPorts[i]);
            }
        }

        /// <summary>
        /// ES: Da formato al DataGrid para crear columnas.
        /// </summary>
        public void LoadDataStructure(DataGrid DG_ColumnList)
        {
            DG_ColumnList.DataContext = _columnStructure;
        }

        /// <summary>
        /// ES: Carga las formas de conexion posible a un ComboBox.
        /// </summary>
        public void LoadConnectionModes(ComboBox CB_ConnectionMode)
        {
            CB_ConnectionMode.Items.Add("Default");
            CB_ConnectionMode.Items.Add("USB");
            CB_ConnectionMode.Items.Add("Bluetooth");
            CB_ConnectionMode.SelectedIndex = 0;
        }

        /// <summary>
        /// ES: Añade una nueva columna a la estructura de datos que se espera recibir.
        /// </summary>
        public void NewColumnToStructure(TextBox TB_Name, TextBox TB_Measure, DataGrid DG_ColumnList)
        {
            DataRow newColumn = _columnStructure.NewRow();
            if (TB_Name.Text == "") return;
            newColumn[0] = TB_Name.Text;
            newColumn[1] = TB_Measure.Text;

            _columnStructure.Rows.Add(newColumn);

            TB_Name.Text = "";
            TB_Measure.Text = "";

            DG_ColumnList.DataContext = _columnStructure;
        }


        public void DeleteColumnToStructure(TextBox TB_Name, TextBox TB_Measure, DataGrid DG_ColumnList)
        {
            int selected = DG_ColumnList.SelectedIndex;
            if (selected == -1) return;
            _columnStructure.Rows.RemoveAt(selected);
            DG_ColumnList.Items.Refresh();

        }


        /// <summary>
        /// ES: Funcion encargada de tabular los datos recogidos, este metodo seguira vigente hasta que la conexion USB se haya cerrado.
        /// </summary>
   /*     private async void USBConnect(DataGrid DG_Monitor, DataTable ProcessedData)
        {
            int lastProcessedRow = 0;
            while (USBConnection.Available())
            {
                for (int i = lastProcessedRow; i < USBConnection.MessagesHistory.RowCount(); ++i)
                {
                    DataRow temp = ProcessedData.NewRow();
                    Debug.WriteLine(USBConnection.MessagesHistory.ColumnCount());
                    for (int j = 0; j < USBConnection.MessagesHistory.ColumnCount(); ++j)
                    {
                        temp[j] = USBConnection.MessagesHistory.Row(i).Values(j);
                    }
                    ProcessedData.Rows.Add(temp);
                    lastProcessedRow++;
                }
                await Task.Delay(100);
            }
        }

        /// <summary>
        /// ES: Funcion encargada de tabular los datos recogidos, este metodo seguira vigente hasta que la conexion Bluetooth se haya cerrado.
        /// </summary>
        private async void BluetoothConnect(DataGrid DG_Monitor, DataTable ProcessedData)
        {
            int lastProcessedRow = 0;
            while (BluetoothConnection.Available())
            {
                for (int i = lastProcessedRow; i < BluetoothConnection.MessagesHistory.RowCount(); ++i)
                {
                    DataRow temp = ProcessedData.NewRow();
                    Debug.WriteLine(BluetoothConnection.MessagesHistory.ColumnCount());
                    for (int j = 0; j < BluetoothConnection.MessagesHistory.ColumnCount(); ++j)
                    {
                        temp[j] = BluetoothConnection.MessagesHistory.Row(i).Values(j);
                    }
                    ProcessedData.Rows.Add(temp);
                    lastProcessedRow++;
                }
                await Task.Delay(100);
            }
        }
        */
        /// <summary>
        /// ES: Se inicia la conexión con el arduino, se analizan y validan los datos básicos.
        /// </summary>
      /*  public bool StartConnection(ComboBox CB_Port, TextBox TB_BaudRate, ComboBox CB_ConnectionMode, DataGrid DG_ColumnList, DataGrid DG_Monitor, Grid G_Serial, Button B_Connect)
        {
            _serialMonitor = new DataTable();

            // Se obiene el Puerto, BaudRate y el modo de conexión
            string port = (string)CB_Port.SelectedItem;
            
            // Se verifica que no se haya dejado vacio el ComboBox
            if (port == null)
            {
                MessageBox.Show("Select port.", "Error.", MessageBoxButton.OK);
                return false;
            }

            int baudRate;
            // Se valida los numeros del TextBox
            if (!Int32.TryParse(TB_BaudRate.Text, out baudRate))
            {
                MessageBox.Show("Check if the BaudRate TextBox are only numbers.", "Cast error.", MessageBoxButton.OK);
                return false;
            }

            // Se guarda el modo de conexion del ComboBox.
            string connectionMode = CB_ConnectionMode.Text;

            // Se obtiene el formato de las columnas y sus medidas.

            // SensorDataArray, que creará un formato, formato que luego será pasado al controlador escogido.
            SDA_Core.Data.SensorDataArray format = new SDA_Core.Data.SensorDataArray();

            // DataTable para tabular los datos, tambien se crea el mismo formato acá
            DataTable table = ((DataView)DG_ColumnList.ItemsSource).ToTable();
            foreach (DataRow row in table.Rows)
            {
                string columnName = (string)row[0], columnMeasure = (string)row[1];
                int tries = 1;
                // Para evitar nombre de columnas repetidas
                while (_serialMonitor.Columns.Contains(columnName))
                {
                    columnName = (string)row[0] + tries.ToString();
                    tries++;
                }

                format.NewColumn(columnName, columnMeasure);
                _serialMonitor.Columns.Add(columnName, typeof(double));
            }

            // Según el método se crea el método de conexión, se asigna el formato y se abre la conexión.
            if (connectionMode == "Default" || connectionMode == "USB")
            {
                USBConnection = new SDA_Core.Communication.USB(port, baudRate);
                USBConnection.MessagesHistory = format;
                USBConnection.Open();
                StartUSBConnection(DG_Monitor);
            }
            else if (connectionMode == "Bluetooth")
            {
                BluetoothConnection = new SDA_Core.Communication.Bluetooth(port, baudRate);
                BluetoothConnection.MessagesHistory = format;
                BluetoothConnection.Open();
                StartBluetoothConnection(DG_Monitor);
            }
            else
            {
                MessageBox.Show("Unknown connection mode.", "Unknown value.", MessageBoxButton.OK);
                return false;
            }

            // Se verifica que se haya abierto la conexión.
            if (!USBConnection.Available())
            {
                MessageBox.Show("Can't connect to serial port.", "Error.", MessageBoxButton.OK);
                return false;
            }

            G_Serial.IsEnabled = false;
            B_Connect.IsEnabled = false;
            return true;
        }
        */
        /// <summary>
        /// ES: Cierra las conexiones activas.
        /// </summary>
        public void CloseConnections(Grid G_Serial, Button B_Connect)
        {
            G_Serial.IsEnabled = true;
            B_Connect.IsEnabled = true;
        }
    }
}
