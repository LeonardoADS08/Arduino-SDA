using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Controls;

namespace SDA_Core.Communication
{
    public class ConnectionManager : Data.Processing
    {
        //private Data.Processing _processer;

        /// <summary>
        /// ES: Comienza una conexión a través de USB en serial con los datos proporcionados.
        /// </summary>
        /// <param name="portName">ES: Nombre del puerto al que está conectado el arduino.</param>
        /// <param name="baudRate">ES: Ratio de baudios (Baud Rate).</param>
        /// <param name="hearInterval">ES: Intervalo de tiempo (ms.) al que se debe escuchar el Serial del arduino.</param>
        public void StartUsbConnection(string portName, int baudRate, int hearInterval = 100)
        {
            //_usbCommunication = new Communication.USB(portName, baudRate, hearInterval);

        }

        /// <summary>
        /// ES: Comienza una conexión a través de bluetooth en serial con los datos proporcionados.
        /// </summary>
        /// <param name="portName">ES: Nombre del puerto al que está conectado el arduino.</param>
        /// <param name="baudRate">ES: Ratio de baudios (Baud Rate).</param>
        /// <param name="hearInterval">ES: Intervalo de tiempo (ms.) al que se debe escuchar el Serial del arduino.</param>
        public void StartBluetoothConnection(string portName, int baudRate, int hearInterval = 100)
        {
           // _bluetoothCommunication = new Communication.Bluetooth(portName, baudRate, hearInterval);

        }
    }
}
