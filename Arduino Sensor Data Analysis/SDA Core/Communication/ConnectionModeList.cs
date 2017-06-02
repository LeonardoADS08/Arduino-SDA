using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Communication
{
    public class ConnectionModeList
    {
        List<ConnectionMode> _connectionModes;

        public List<ConnectionMode> ConnectionModes
        {
            get { return _connectionModes; }
            set { _connectionModes = value; }
        }

        public ConnectionModeList()
        {
            _connectionModes = new List<ConnectionMode>();
        }
    }
}
