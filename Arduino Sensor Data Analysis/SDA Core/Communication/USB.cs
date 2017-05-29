using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SDA_Core.Communication
{
    public class USB : Serial
    {
        public USB()
        {

        }

        public USB(string portName, int baudRate, int hearInterval = 100) : base(portName, baudRate, hearInterval)
        {

        }
    }
}
