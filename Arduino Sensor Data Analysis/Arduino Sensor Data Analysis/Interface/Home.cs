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
    class Home 
    {
        public Home()
        {
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
            for (int i = 0; i < SDA_Core.Program.BaudRatesList.BaudRates.Count; ++i)
            {
                CB_Sensors.Items.Add(SDA_Core.Program.SensorList.SensorList[i].SensorName);
            }
        }
      
    }
}
