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
using SDA_Core.Communication;
using SDA_Core.Data.Containers;

namespace SDA_Program.Interface
{
    class Home 
    {
        // Para conectarse con un arduino
        private SDA_Core.Communication.SerialConnection _serialConnection;
        private DataTable _serialMonitor;
        private SDA_Core.Data.Processing _processer;
        private SDA_Core.Data.Containers.SensorData _receivedData;
        private Thread _thread;

        public DataTable SerialMonitor
        {
            get { return _serialMonitor; }
            set { _serialMonitor = value; }
        }

        public SerialConnection SerialConnection
        {
            get { return _serialConnection; }
            set { _serialConnection = value; }
        }

        public SensorData ReceivedData
        {
            get { return _receivedData; }
            set { _receivedData = value; }
        }

        public Home()
        {
            _serialMonitor = new DataTable();
            _processer = new SDA_Core.Data.Processing();

            _thread = new Thread(() => SerialCommunication());
        }


        private async void SerialCommunication()
        {
            List<SDA_Core.Data.Containers.Measurement> formatList = SDA_Core.Program.GetSensorsFormat();
            _receivedData = new SDA_Core.Data.Containers.SensorData(formatList.Count);
            while (_serialConnection.Available())
            {
                _processer.Process(_serialConnection.Read(), ref _receivedData, formatList);
                SDA_Core.Program.ReceivedData = _receivedData;
                SDA_Core.Program.SerialMonitor = _receivedData.ToDataTable();
                await Task.Delay(100);
            }
        }

        /// <summary>
        /// ES: Método para cargar los puertos serial disponibles en un ComboBox.
        /// </summary>
        public void LoadSerialPorts(ComboBox CB_Ports)
        {
            CB_Ports.Items.Clear();
            SDA_Core.Communication.SerialConnection serial = new SDA_Core.Communication.SerialConnection();
            List<string> AvailableSerialPorts = serial.SerialPorts();
            for (int i = 0; i < AvailableSerialPorts.Count; ++i)
            {
                CB_Ports.Items.Add(AvailableSerialPorts[i]);
            }
        }

        public void LoadBaudRates(ComboBox CB_BaudRates)
        {
            CB_BaudRates.Items.Clear();
            for (int i = 0; i < SDA_Core.Program.BaudRatesList.BaudRates.Count; ++i)
            {
                CB_BaudRates.Items.Add(SDA_Core.Program.BaudRatesList.BaudRates[i].BaudRate.ToString());
            }
        }

        public void LoadSensors(ComboBox CB_Sensors)
        {
            CB_Sensors.Items.Clear();
            for (int i = 0; i < SDA_Core.Program.SensorList.SensorList.Count; ++i)
            {
                CB_Sensors.Items.Add(SDA_Core.Program.SensorList.SensorList[i].SensorName);
            }
        }

        public void LoadReceivedSensors(DataGrid DG_ReceivedSensors)
        {
            DG_ReceivedSensors.ItemsSource = SDA_Core.Program.ReceivedSensorsToDataTable().AsDataView();
        }

        public void SaveReceivedSensor(DataGrid DG_ReceivedSensors, ComboBox CB_Sensors)
        {
            if (CB_Sensors.SelectedItem != null)
            {
                int selected = CB_Sensors.SelectedIndex;
                var newSensor = SDA_Core.Program.SensorList.SensorList[selected];
                SDA_Core.Program.SensorsFormat.SensorList.Add(newSensor);
                LoadReceivedSensors(DG_ReceivedSensors);
            }
        }

        public void DeleteReceivedSensor(DataGrid DG_ReceivedSensor)
        {
            if (DG_ReceivedSensor.SelectedItem != null)
            {
                int selected = DG_ReceivedSensor.SelectedIndex;
                SDA_Core.Program.SensorsFormat.SensorList.RemoveAt(selected);
                LoadReceivedSensors(DG_ReceivedSensor);
            }
        }

        public bool StartConnection(ComboBox CB_Ports, ComboBox CB_BaudRates)
        {
            if (CB_Ports.SelectedValue == null || CB_BaudRates.SelectedValue == null)
            {
                MessageBox.Show("Check connection parameters.", "Error,", MessageBoxButton.OK);
                return false;
            }
            string port = (string)CB_Ports.Items[CB_Ports.SelectedIndex];
            int baudRateValue = int.Parse((string)CB_BaudRates.Items[CB_BaudRates.SelectedIndex]);
            SDA_Core.Communication.BaudRates baudRate= new SDA_Core.Communication.BaudRates(baudRateValue);

            _serialConnection = new SDA_Core.Communication.SerialConnection(port, baudRate);
            _serialConnection.Open();
            _thread.Start();
            return _serialConnection.Available();
        }

        public void StopConnection()
        {
            _serialConnection.Close();
        }

        public async void CheckLists(ComboBox CB_Ports, ComboBox CB_BaudRates, ComboBox CB_SensorLists)
        {
            while (true)
            {
                if (SDA_Core.Program.ReloadLists)
                {
                    LoadSerialPorts(CB_Ports);
                    LoadBaudRates(CB_BaudRates);
                    LoadSensors(CB_SensorLists);
                    SDA_Core.Program.ReloadLists = false;
                }
                await Task.Delay(1000);
            }
        }
      
    }
}
