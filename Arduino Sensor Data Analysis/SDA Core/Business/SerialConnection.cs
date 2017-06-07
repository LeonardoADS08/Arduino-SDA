using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business
{
    public class SerialConnection
    {
        private BaudRates _baudRate;
        private Port _port;

        public BaudRates BaudRate { get => _baudRate; set => _baudRate = value; }
        public Port Port { get => _port; set => _port = value; }

        public SerialConnection()
        {
            _baudRate = new BaudRates();
            _port = new Port();
        }

        public SerialConnection(BaudRates baudRate, Port portName)
        {
            _baudRate = baudRate;
            _port = portName;
        }

        
    }
}
