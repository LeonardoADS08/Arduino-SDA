using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace SDA_Core.Communication
{   
    /// <summary>
    /// ES: Clase para contener configuraciones de conexión.
    /// </summary>
    public class ConnectionMode
    {
        private Parity _parity;
        private int _dataBits;
        private StopBits _stopBits;

        public ConnectionMode()
        {
            SerialPort def = new SerialPort();
            _parity = def.Parity;
            _dataBits = def.DataBits;
            _stopBits = def.StopBits;
        }
    }
}
