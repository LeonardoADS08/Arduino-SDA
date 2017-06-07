using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business
{
    [Serializable]
    public class Port
    {
        private string _name;

        public string Name { get => _name; set => _name = value; }

        public Port()
        {
            _name = "";
        }

        public Port(string portName)
        {
            _name = portName;
        }

    }
}
