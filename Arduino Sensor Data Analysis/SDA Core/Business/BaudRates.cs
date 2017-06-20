using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business
{
    [Serializable]
    public class BaudRates
    {
        private int _IdBaudRate;
        private int _value;

        public int Value { get => _value; set => _value = value; }
        public int IdBaudRate { get => _IdBaudRate; set => _IdBaudRate = value; }

        public BaudRates()
        {
            _IdBaudRate = -1;
            _value = 0;
        }

        public BaudRates(int baudrate)
        {
            _IdBaudRate = -1;
            _value = baudrate;
        }

    }
}
