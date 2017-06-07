using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Communication
{
    [Serializable]
    public class BaudRatesOld
    {
        private int _baudRate;

        public int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        public BaudRatesOld()
        {
            _baudRate = 0;
        }

        public BaudRatesOld(int baudrate)
        {
            _baudRate = baudrate;
        }
    }
}
