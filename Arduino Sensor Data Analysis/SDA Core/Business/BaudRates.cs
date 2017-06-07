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
        private int _value;

        public int Value { get => _value; set => _value = value; }

        public BaudRates()
        {
            _value = 0;
        }

        public BaudRates(int baudrate)
        {
            _value = baudrate;
        }

    }
}
