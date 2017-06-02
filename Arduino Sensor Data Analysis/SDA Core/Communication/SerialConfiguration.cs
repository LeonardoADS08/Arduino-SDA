using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Communication
{
    public class SerialConfiguration
    {
        private string _port;
        private int _baudRate;

        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        public SerialConfiguration()
        {
            _port = "";
            _baudRate = 0;
        }

        public SerialConfiguration(string port, int baudrate)
        {
            _port = port;
            _baudRate = baudrate;
        }
    }
}
