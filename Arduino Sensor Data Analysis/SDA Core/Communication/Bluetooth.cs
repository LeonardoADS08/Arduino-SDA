using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SDA_Core.Communication
{
    public class Bluetooth : Serial
    {

        public Bluetooth()
        {
            Initialize();
        }

        public void Initialize()
        {
            SerialPort _configurations = new SerialPort();
            _configurations.Parity = Parity.None;
            _configurations.DataBits = 8;
            _configurations.StopBits = StopBits.One;
            SerialConnection = _configurations;
        }

        public Bluetooth(string portName, int baudRate, int hearInterval = 100) : base(portName, baudRate, hearInterval)
        {
            Initialize();
        }
    }
}
