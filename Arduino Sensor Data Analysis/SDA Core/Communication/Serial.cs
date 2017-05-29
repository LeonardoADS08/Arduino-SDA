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
    public class Serial : Connection
    {
        private SerialPort _serialConnection;
        private SensorDataArray _messagesHistory;
        private Thread _thread;
        private Processing _processer;

        protected SerialPort SerialConnection
        {
            get { return _serialConnection; }
            set { _serialConnection = value; }
        }

        /// <summary>
        /// ES: Es la lista que contiene todos los mensajes recibidos durante la comunicación que se ha hecho con el arduino, esta lista se actualiza cada vez que se encuentra un nuevo mensaje.
        /// </summary>
        public SensorDataArray MessagesHistory
        {
            get { return _messagesHistory; }
            set { _messagesHistory = value; }
        }

        /// <summary>
        /// ES: Constructor de la clase Serial.
        /// </summary>
        public Serial()
        {
            _serialConnection = new SerialPort();
            _thread = new Thread(() => Hear(100));
            _messagesHistory = new SensorDataArray();
            _processer = new Processing();
        }

        /// <summary>
        /// ES: Constructor de la clase Serial, preparada para una conexión.
        /// </summary>
        /// <param name="portName">ES: Nombre del puerto al que está conectado el arduino.</param>
        /// <param name="baudRate">ES: Ratio de baudios (Baud Rate).</param>
        /// <param name="hearInterval">ES: Intervalo de tiempo (ms.) al que se debe escuchar el Serial del arduino.</param>
        public Serial(string portName, int baudRate, int hearInterval = 100)
        {
            _serialConnection = new SerialPort(portName, baudRate);
            _thread = new Thread(() => Hear(hearInterval));
            _messagesHistory = new SensorDataArray();
            _processer = new Processing();
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
                    _processer.Process(data, ref _messagesHistory);
                }
                await Task.Delay(timeInterval);
            }
            // ES: Si por alguna razón deja de escuchar, se termina la conexión.
            _thread.Abort();
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
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Write()"); }
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
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Write()"); }
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
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Available()"); }
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
                    _thread.Start();
                }
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Open()"); }
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
                    _thread.Abort();
                }
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Close()"); }
        }

        /// <summary>
        /// ES: Enlista todos los puertos disponibles.
        /// </summary>
        /// <returns>ES: GenericArray<string> Con todos los puertos disponibles para una comunicación Serial. </returns>
        public Data.GenericArray<string> SerialPorts()
        {
            Data.GenericArray<string> res = new Data.GenericArray<string>();
            res.FromDefaultArray( SerialPort.GetPortNames() );
            return res;
        }

    }
}
