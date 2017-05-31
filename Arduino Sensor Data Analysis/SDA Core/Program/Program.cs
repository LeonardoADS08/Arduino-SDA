using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDA_Core.Analysis;
using SDA_Core.Communication;

namespace SDA_Core
{
    public class Program
    {
        static private Analysis.Statistics _statistics;
        static private Communication.USB _usbConnection;
        static private Communication.Bluetooth _bluetoothConnection;

        public static Statistics Statistics
        {
            get { return _statistics; }
            set { _statistics = value; }
        }

        public static USB USBConnection
        {
            get { return _usbConnection; }
            set { _usbConnection = value; }
        }

        public static Bluetooth BluetoothConnection
        {
            get { return _bluetoothConnection; }
            set { _bluetoothConnection = value; }
        }

        static Program()
        {
            _usbConnection = new USB();
            _bluetoothConnection = new Bluetooth();
            _statistics = new Statistics();
        }

        public Program()
        {
            
        }
    }
}
