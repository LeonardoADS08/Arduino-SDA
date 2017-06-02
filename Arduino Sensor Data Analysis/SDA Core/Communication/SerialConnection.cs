using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Controls;
using SDA_Core.Data;

namespace SDA_Core.Communication
{
    /// <summary>
    /// ES: Clase para la comunicación serial con un arduino, la clase utiliza un hilo independiente especialmente para escuchar los datos que envia el arduino.
    /// </summary>
    public class SerialConnection : Connection
    {
        private SerialConfiguration _serialConfiguration;
        private SerialPort _serialConnection;

        /// <summary>
        /// ES: Constructor de la clase Serial.
        /// </summary>
        public SerialConnection()
        {
            _serialConfiguration = new SerialConfiguration();
            _serialConnection = new SerialPort();

        }

        /// <summary>
        /// ES: Constructor de la clase Serial, preparada para una conexión.
        /// </summary>
        /// <param name="portName">ES: Nombre del puerto al que está conectado el arduino.</param>
        /// <param name="baudRate">ES: Ratio de baudios (Baud Rate).</param>
        /// <param name="hearInterval">ES: Intervalo de tiempo (ms.) al que se debe escuchar el Serial del arduino.</param>
        public SerialConnection(SerialConfiguration configurations)
        {
            _serialConnection = new SerialPort(configurations.Port, configurations.BaudRate);
        }

        /// <summary>
        /// ES: Escucha lo que el arduino está enviando, este método es asíncrono y se ejecutará hasta que se indique o hasta que falle la conexión.
        /// </summary>
        /// <param name="timeInterval">ES: Intervalo de tiempo (ms.) al que se debe escuchar el Serial del arduino.</param>
        private async void Hear(int timeInterval = 100)
        {
            string data;
            bool firstData = true;
            while (Available())
            {
                data = Read();
                if (data != "")
                {
                    // Se rechaza el primer dato recibido.
                    if (firstData)
                    {
                        firstData = false;
                        continue;
                    }
               //     _processer.Process(data, ref _messagesHistory);
                }
                await Task.Delay(timeInterval);
            }
            // ES: Si por alguna razón deja de escuchar, se termina la conexión.
            //_thread.Abort();
        }

        /// <summary>
        /// ES: Lee una linea del 'buffer' que existe en el serial del arduino.
        /// </summary>
        /// <returns>ES: Devuelve una linea o vacio.</returns>
        public override string Read()
        {
            try
            {
                if (!_serialConnection.IsOpen) _serialConnection.Open();
                return _serialConnection.ReadLine();
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(SerialConnection).FullName + ".Write()"); }
            return "";
        }

        /// <summary>
        /// ES: Envia un mensaje al serial del arduino.
        /// </summary>
        /// <param name="message">ES: Mensaje a enviar.</param>
        public override void Write(string message)
        {
            try
            {
                if (!_serialConnection.IsOpen) _serialConnection.Open();
                _serialConnection.Write(message);
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(SerialConnection).FullName + ".Write()"); }
        }

        
        /// <summary>
        /// ES: Pregunta si el puerto esta disponible para comunicación.
        /// </summary>
        /// <returns>ES: Estado de la conexión.</returns>
        public override bool Available()
        {
            try
            {
                return _serialConnection.IsOpen;
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(SerialConnection).FullName + ".Available()"); }
            return false;
        }

        /// <summary>
        /// ES: Comienza si no esta abierta la comunicación con el arduino con los parametros dados.
        /// </summary>
        public override void Open()
        {
            try
            {
                if (!_serialConnection.IsOpen)
                {
                    _serialConnection.Open();
                    //_thread.Start();
                }
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(SerialConnection).FullName + ".Open()"); }
        }   

        /// <summary>
        /// ES: Cierra si esta abierta la conexión con el arduino.
        /// </summary>
        public override void Close()
        {
            try
            {
                if (_serialConnection.IsOpen)
                {
                    _serialConnection.Close();
                    //_thread.Abort();
                }
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(SerialConnection).FullName + ".Close()"); }
        }

        /// <summary>
        /// ES: Enlista todos los puertos disponibles.
        /// </summary>
        /// <returns>ES: GenericArray<string> Con todos los puertos disponibles para una comunicación Serial. </returns>
        public List<string> SerialPorts()
        {
            List<string> serialPorts = new List<string>();
            serialPorts = SerialPort.GetPortNames().ToList();
            return serialPorts;
        }

    }
}
