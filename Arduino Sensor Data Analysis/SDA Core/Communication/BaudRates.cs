using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Communication
{
    public class BaudRates
    {
        private int _baudRate;

        public int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        public BaudRates()
        {
            _baudRate = 0;
        }

        public BaudRates(int baudrate)
        {
            _baudRate = baudrate;
        }
    }
}
