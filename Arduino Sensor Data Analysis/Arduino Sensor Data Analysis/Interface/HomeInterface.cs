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
        // Intervalo al que se debe escuchar al arduino
        int timeInterval = 1000;

        // Objeto para realizar la conexión Serial.
        private SerialPort serialConnection;
        // Objeto de una clase 'funcional' para procesar los datos recibidos por el arduino
        private SDA_Core.Functional.Processing processer;
        // Obeto que almacena la estructura de sensores que recibira del arduino durante la comunicacion.
        private SDA_Core.Business.Arrays.SensorDataArray selectedSensors;
        // Objeto de una clase 'funcional' para hacer transformaciones.
        private SDA_Core.Functional.Data dataManager;
        // Objeto que almacena todos los datos procesados que se han recibido del arduino.
        private SDA_Core.Business.Arrays.SensorData communication;

        /// <summary>
        /// ES: Constructor de la clase HomeInterface.
        /// </summary>
        public HomeInterface()
        {
            selectedSensors = new SDA_Core.Business.Arrays.SensorDataArray();
            dataManager = new SDA_Core.Functional.Data();
            processer = new SDA_Core.Functional.Processing();
            communication = new SDA_Core.Business.Arrays.SensorData();
        }

        /// <summary>
        /// ES: Carga los puertos serial a un comboBox.
        /// </summary>
        /// <param name="CB_SerialPorts">ES: ComboBox donde se deben cargar los datos</param>
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

        /// <summary>
        /// ES: Carga los baudRates a un comboBox.
        /// </summary>
        /// <param name="CB_BaudRates">ES: ComboBox donde se deben cargar los datos</param>
        /// <param name="data">ES: Datos a cargar.</param>
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

        /// <summary>
        /// ES: Carga los sensores a un comboBox.
        /// </summary>
        /// <param name="CB_Sensors">ES: ComboBox donde se deben cargar los datos</param>
        /// <param name="data">ES: Datos a cargar.</param>
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

        /// <summary>
        /// ES: Elimina un sensor de lo seleccionados.
        /// </summary>
        /// <param name="DG_SensorList">ES: DataGrid donde se deben realizar los cambios.</param>
        public void DeleteSensor(DataGrid DG_SensorList)
        {
            if (DG_SensorList.SelectedItem == null) return;
            int selected = DG_SensorList.SelectedIndex;
            selectedSensors.List.Remove(selected);
            UpdateTable(DG_SensorList);
        }

        /// <summary>
        /// ES: A partir de un comboBox añade el seleccionado a la estructura de datos recibidos.
        /// </summary>
        /// <param name="DG_SensorList">ES: DataGrid donde se deben realizar los cambios.</param>
        /// <param name="CB_Sensors">ES: ComboBox donde se encuentran los sensores.</param>
        public void AddSensor(DataGrid DG_SensorList, ComboBox CB_Sensors)
        {
            // Validaciones
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

            // Se añade el nuevo sensor, se reinicializa el comboBox y se actualiza la tabla
            selectedSensors.List.Add(sensor);
            CB_Sensors.SelectedIndex = -1;
            UpdateTable(DG_SensorList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DG_SerialMonitor">ES: DataGrid donde se deben realizar los cambios.</param>
        /// <param name="CB_SerialPort">ES: ComboBox donde se encuentra el serialPort seleccionado.</param>
        /// <param name="CB_BaudRate">ES: ComboBox donde se encuentran el baudRate seleccionado.</param>
        /// <param name="G_Serial">ES: Grid a bloquear cambios a las configuraciones durante la comunicación.</param>
        /// <param name="G_Sensors">ES: Grid a bloquear cambios a las configuraciones durante la comunicación.</param>
        /// <param name="B_Connect">ES: Botón a bloquear cambios a las configuraciones durante la comunicación.</param>
        public void StartConnection(DataGrid DG_SerialMonitor, ComboBox CB_SerialPort, ComboBox CB_BaudRate, Grid G_Serial, Grid G_Sensors, Button B_Connect)
        {
            // Validaciones 
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
            // Se limpia el SerialMonitor
            DG_SerialMonitor.DataContext = null;

            // Se obtiene el puerto y el baudRate
            SDA_Core.Business.Port port = (SDA_Core.Business.Port)CB_SerialPort.SelectedItem;
            SDA_Core.Business.BaudRates baudrate = (SDA_Core.Business.BaudRates)CB_BaudRate.SelectedItem;

            // Se inicializa el objeto que recibira los datos procesados.
            communication = new SDA_Core.Business.Arrays.SensorData();
            for (int i = 0; i < selectedSensors.List.Size; ++i)
            {
                for (int j = 0; j < selectedSensors.List[i].Columns.Size; ++j)
                {
                    communication.Columns.Add(selectedSensors.List[i].Columns[j]);
                }
            }

            // Se inicializa el objeto que realizará la conexión a serial.
            serialConnection = new SerialPort(port.Name, baudrate.Value);

            // Se intenta la conexión con el arduino.
            try
            {
                // Si se abre la conexion, se bloquea el interfaz de configuraciones y en un nuevo hilo se abre el método que escuchara y procesará los datos del arduino.
                serialConnection.Open();
                if (serialConnection.IsOpen)
                {
                    Thread thread = new Thread(() => OpenSerialConnection());
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

        /// <summary>
        /// ES: Método asincrónico que escucha un arduino y procesa los datos recibidos.
        /// </summary>
        private async void OpenSerialConnection()
        {
            while (serialConnection.IsOpen)
            {
                try
                {
                    string message = serialConnection.ReadLine();
                    if (message != "") processer.Process(message, communication);
                }
                catch (Exception ex) { return; }
                
                await Task.Delay(timeInterval);
            }
        }

        /// <summary>
        /// ES: Método asincrónico que actualiza el serialMonitor con los nuevos datos recibidos..
        /// </summary>
        private async void UpdateMonitor(DataGrid DG_SerialMonitor)
        {
            DataTable tableData = new DataTable();
            DG_SerialMonitor.DataContext = tableData;
            bool formatSet = false;

            while (serialConnection.IsOpen)
            {
                dataManager.SensorDataToDataTable(ref tableData, communication);

                if (!formatSet && tableData.Columns.Count != 0)
                {
                    formatSet = true;
                    DG_SerialMonitor.DataContext = tableData;
                }

                await Task.Delay(timeInterval);
            }
        }

        /// <summary>
        /// ES: Método para detener la conexión manualmente.
        /// </summary>
        /// <param name="G_Serial">ES: Grid a desbloquear para reactivar las configuraciones.</param>
        /// <param name="G_Sensors">ES: Grid a desbloquear para reactivar las configuraciones.</param>
        /// <param name="B_Connect">ES: Botón a desbloquear para reactivar las configuraciones.</param>
        public void StopConnection(Grid G_Serial, Grid G_Sensors, Button B_Connect)
        {
            if (serialConnection == null) return;
            if (serialConnection.IsOpen) serialConnection.Close();
            G_Sensors.IsEnabled = true;
            G_Serial.IsEnabled = true;
            B_Connect.IsEnabled = true;
        }

        /// <summary>
        /// ES: Obtiene los datos que se han obtenido durante la comunicación.
        /// </summary>
        /// <returns>ES: Datos del serialMonitor.</returns>
        public SDA_Core.Business.Arrays.SensorData GetSerialMonitorData() => communication;

        /// <summary>
        /// ES: Estado de la conexión con el arduino.
        /// </summary>
        public bool ConnectionState()
        {
            if (serialConnection != null) return serialConnection.IsOpen;
            return false;
        }

        /// <summary>
        /// ES: Método encargado de desbloquear el interfaz si es que la conexión se finaliza subitamente.
        /// </summary>
        public async void CheckConnection(Grid G_Serial, Grid G_Sensors, Button B_Connect)
        {
            while (true)
            {
                if (!ConnectionState()) StopConnection(G_Serial, G_Sensors, B_Connect);
                await Task.Delay(1000);
            }
        }

        /// <summary>
        /// ES: Método para actualizar un dataGrid.
        /// </summary>
        /// <param name="DG_SensorList">ES: DataGrid a realizar los cambios.</param>
        public void UpdateTable(DataGrid DG_SensorList) => DG_SensorList.ItemsSource = dataManager.SensorsListToDataTable(selectedSensors).AsDataView();
    }
}
