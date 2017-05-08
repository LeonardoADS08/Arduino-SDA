using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SDA_Core.Communication
{
    /// <summary>
    /// ES: Clase para la comunicación serial con un arduino, es recomendable usar esta clase en un hilo paralelo.
    /// </summary>
    public class Serial
    {
        SerialPort _serialConnection;

        public Serial(string portName, int baudRate)
        {
            _serialConnection = new SerialPort();
            _serialConnection.PortName = portName;
            _serialConnection.BaudRate = baudRate;
        }

        public void Write(string message)
        {
            try
            {
                if (!_serialConnection.IsOpen) _serialConnection.Open();
                _serialConnection.Write(message);
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Write()"); }
        }

        public string Read(string message)
        {
            try
            {
                if (!_serialConnection.IsOpen) _serialConnection.Open();
                return _serialConnection.ReadLine();
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Write()"); }
            return "";
        }

        public bool Available()
        {
            try
            {
                return _serialConnection.IsOpen;
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Available()"); }
            return false;
        }

        public void Open()
        {
            try
            {
                if (!_serialConnection.IsOpen) _serialConnection.Open();
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Open()"); }
        }   

        public void Close()
        {
            try
            {
                if (_serialConnection.IsOpen) _serialConnection.Close();
            }
            catch (Exception ex) { Data.RuntimeLogs.SendLog(ex.Message, typeof(Serial).FullName + ".Close()"); }
        }

    }
}
