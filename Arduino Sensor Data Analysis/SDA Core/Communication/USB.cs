using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Communication
{
    class USB : Serial
    {
        public USB(string portName, int baudRate, int hearInterval = 100) : base(portName, baudRate, hearInterval)
        {

        }
    }
}
