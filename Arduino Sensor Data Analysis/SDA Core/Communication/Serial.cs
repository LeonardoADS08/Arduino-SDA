using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Controls;

namespace SDA_Core.Communication
{
    /// <summary>
    /// ES: Clase para la comunicación serial con un arduino, la clase utiliza un hilo independiente especialmente para escuchar los datos que envia el arduino.
    /// </summary>
    public class Serial
    {
        private SerialPort _serialConnection;
        private string _communicationHistory = "";
        private Thread _thread;

        public string CommunicationHistory
        {
            get { return _communicationHistory; }
        }

        /// <summary>
        /// ES: Constructor de la clase Serial.
        /// </summary>
        /// <param name="portName">ES: Nombre del puerto al que está conectado el arduino.</param>
        /// <param name="baudRate">ES: Ratio de baudios (Baud Rate).</param>
        /// <param name="hearInterval">ES: Intervalo de tiempo (ms.) al que se debe escuchar el Serial del arduino.</param>
        public Serial(string portName, int baudRate, int hearInterval = 100)
        {
            _serialConnection = new SerialPort(portName, baudRate);
            _thread = new Thread(() => Hear(hearInterval));
        }

        /// <summary>
        /// ES: Escucha lo que el arduino está enviando.
        /// </summary>
        /// <param name="timeInterval">ES: Intervalo de tiempo (ms.) al que se debe escuchar el Serial del arduino.</param>
        private async void Hear(int timeInterval = 100)
        {
            string data;
            while (Available())
            {
                data = Read();
                if (data != "") _communicationHistory += data + "\n";
                await Task.Delay(timeInterval);
            }
            // ES: Si por alguna razón deja de escuchar, se termina la conexión.
            _thread.Abort();
        }

        /// <summary>
        /// ES: Lee una linea del 'buffer' que existe en el serial del arduino.
        /// </summary>
        private string Read()
        {
            try
            {
                if (!_serialConnection.IsOpen) _serialConnection.Open();
                return _serialConnection.ReadLine();
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Write()"); }
            return "";
        }

        /// <summary>
        /// ES: Envia un mensaje al serial del arduino.
        /// </summary>
        /// <param name="message">ES: Mensaje a enviar.</param>
        public void Write(string message)
        {
            try
            {
                if (!_serialConnection.IsOpen) _serialConnection.Open();
                _serialConnection.Write(message);
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Write()"); }
        }

        
        /// <summary>
        /// ES: Pregunta si el puerto esta disponible para comunicación.
        /// </summary>
        public bool Available()
        {
            try
            {
                return _serialConnection.IsOpen;
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Available()"); }
            return false;
        }

        /// <summary>
        /// ES: Comienza si no esta abierta la comunicación con el arduino con los parametros dados.
        /// </summary>
        public void Open()
        {
            try
            {
                if (!_serialConnection.IsOpen)
                {
                    _serialConnection.Open();
                    _thread.Start();
                }
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Open()"); }
        }   

        /// <summary>
        /// ES: Cierra si esta abierta la conexión con el arduino.
        /// </summary>
        public void Close()
        {
            try
            {
                if (_serialConnection.IsOpen)
                {
                    _serialConnection.Close();
                    _thread.Abort();
                }
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Close()"); }
        }

    }
}
