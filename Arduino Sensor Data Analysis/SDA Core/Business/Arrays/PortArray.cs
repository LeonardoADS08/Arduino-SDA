using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business.Arrays
{
    public class PortArray
    {
        private GenericArray<Port> _list;

        public GenericArray<Port> List { get => _list; set => _list = value; }

        public PortArray()
        {
            _list = new GenericArray<Port>();
        }
    }
}
