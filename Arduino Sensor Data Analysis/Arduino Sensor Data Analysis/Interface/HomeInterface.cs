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
using System.Windows.Shapes;
using System.Data;
using System.IO.Ports;
using System.Diagnostics;
using SDA_Core;

namespace SDA_Program.Interface
{
    public class HomeInterface
    {
        private SerialPort serialConnection;
        private SDA_Core.Functional.Processing processer;
        private SDA_Core.Business.Arrays.SensorDataArray selectedSensors;
        private SDA_Core.Functional.Data dataManager;
        private SDA_Core.Business.Arrays.SensorData communication;

        public HomeInterface()
        {
            selectedSensors = new SDA_Core.Business.Arrays.SensorDataArray();
            dataManager = new SDA_Core.Functional.Data();
            processer = new SDA_Core.Functional.Processing();
            communication = new SDA_Core.Business.Arrays.SensorData();
        }

        public void LoadSerialPorts(ComboBox CB_SerialPorts)
        {
            CB_SerialPorts.Items.Clear();
            CB_SerialPorts.DisplayMemberPath = "Name";
            CB_SerialPorts.SelectedValuePath = "Name";
            List<string> availablesPorts = SerialPort.GetPortNames().ToList();
            foreach (string port in availablesPorts)
            {
                SDA_Core.Business.Port newPort = new SDA_Core.Business.Port(port);
                CB_SerialPorts.Items.Add(newPort);
            }
        }

        public void LoadBaudRates(ComboBox CB_BaudRates, SDA_Core.Business.Arrays.BaudRatesArray data)
        {
            CB_BaudRates.Items.Clear();
            CB_BaudRates.DisplayMemberPath = "Value";
            CB_BaudRates.SelectedValuePath = "Value";
            for (int i = 0; i < data.List.Size; ++i)
            {
                SDA_Core.Business.BaudRates value = data.List[i];
                CB_BaudRates.Items.Add(value);
            }
        }

        public void LoadSensors(ComboBox CB_Sensors, SDA_Core.Business.Arrays.SensorDataArray data)
        {
            CB_Sensors.Items.Clear();
            CB_Sensors.DisplayMemberPath = "SensorName";
            CB_Sensors.SelectedValuePath = "SensorName";
            for (int i = 0; i < data.List.Size; ++i)
            {
                SDA_Core.Business.Arrays.SensorData value = data.List[i];
                CB_Sensors.Items.Add(value);
            }
        }

        public void DeleteSensor(DataGrid DG_SensorList)
        {
            if (DG_SensorList.SelectedItem == null) return;
            int selected = DG_SensorList.SelectedIndex;
            selectedSensors.List.Remove(selected);
            UpdateTable(DG_SensorList);
        }

        public void AddSensor(DataGrid DG_SensorList, ComboBox CB_Sensors)
        {
            if (CB_Sensors.SelectedItem == null)
            {
                MessageBox.Show("Empty fields found.", "Error", MessageBoxButton.OK);
                return;
            }

            SDA_Core.Business.Arrays.SensorData sensor = (SDA_Core.Business.Arrays.SensorData)CB_Sensors.SelectedItem;
            if (selectedSensors.List.Exists(sensor))
            {
                MessageBox.Show("This sensor alredy exists.", "Error", MessageBoxButton.OK);
                return;
            }

            selectedSensors.List.Add(sensor);
            UpdateTable(DG_SensorList);
        }

        public void StartConnection(DataGrid DG_SerialMonitor, ComboBox CB_SerialPort, ComboBox CB_BaudRate, Grid G_Serial, Grid G_Sensors, Button B_Connect)
        {
            if (CB_SerialPort.SelectedItem == null)
            {
                MessageBox.Show("Select a port.", "Error", MessageBoxButton.OK);
                return;
            }
            if (CB_BaudRate.SelectedItem == null)
            {
                MessageBox.Show("Select a baud rate.", "Error", MessageBoxButton.OK);
                return;
            }
            if (selectedSensors.List.Size == 0)
            {
                MessageBox.Show("Not selected sensors.", "Error", MessageBoxButton.OK);
                return;
            }
            DG_SerialMonitor.DataContext = null;
            SDA_Core.Business.Port port = (SDA_Core.Business.Port)CB_SerialPort.SelectedItem;
            SDA_Core.Business.BaudRates baudrate = (SDA_Core.Business.BaudRates)CB_BaudRate.SelectedItem;

            communication = new SDA_Core.Business.Arrays.SensorData();
            for (int i = 0; i < selectedSensors.List.Size; ++i)
            {
                for (int j = 0; j < selectedSensors.List[i].Columns.Size; ++j)
                {
                    communication.Columns.Add(selectedSensors.List[i].Columns[j]);
                }
            }

            serialConnection = new SerialPort(port.Name, baudrate.Value);

            try
            {
                serialConnection.Open();
                if (serialConnection.IsOpen)
                {
                    Thread thread = new Thread(() => OpenSerialConnection(DG_SerialMonitor));
                    thread.Start();
                    UpdateMonitor(DG_SerialMonitor);
                    G_Sensors.IsEnabled = false;
                    G_Serial.IsEnabled = false;
                    B_Connect.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Can't connect.", "Unknown error", MessageBoxButton.OK);
                    return;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception found", MessageBoxButton.OK);
                return;
            }

        }

        private async void OpenSerialConnection(DataGrid DG_SerialMonitor)
        {
            while (serialConnection.IsOpen)
            {
                try
                {
                    string message = serialConnection.ReadLine();
                    if (message != "") processer.Process(message, ref communication);
                }
                catch (Exception ex) { return; }
                
                await Task.Delay(100);
            }
        }

        private async void UpdateMonitor(DataGrid DG_SerialMonitor)
        {
            while (serialConnection.IsOpen)
            {
                if (DG_SerialMonitor.Items.Count == 0)
                    DG_SerialMonitor.DataContext = dataManager.SensorDataToDataTable(communication);
                else
                {
                    DG_SerialMonitor.DataContext = dataManager.UpdateSensorDataDataTable(((DataView)DG_SerialMonitor.ItemsSource).ToTable(), communication);
                    //dataManager.UpdateSensorDataDataTable(DG_SerialMonitor, communication);
                }
                await Task.Delay(150);
            }
        }

        public void StopConnection(Grid G_Serial, Grid G_Sensors, Button B_Connect)
        {
            if (serialConnection.IsOpen) serialConnection.Close();
            G_Sensors.IsEnabled = true;
            G_Serial.IsEnabled = true;
            B_Connect.IsEnabled = true;
        }

        public SDA_Core.Business.Arrays.SensorData GetSerialMonitorData() => communication;

        public void UpdateTable(DataGrid DG_SensorList) => DG_SensorList.ItemsSource = dataManager.SensorsListToDataTable(selectedSensors).AsDataView();
    }
}
