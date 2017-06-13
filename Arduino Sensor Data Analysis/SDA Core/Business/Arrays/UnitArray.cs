using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business.Arrays
{
    public class UnitArray : Unit
    {
        private GenericArray<Unit> _list;

        public GenericArray<Unit> List { get => _list; set => _list = value; }

        public UnitArray()
        {
            _list = new GenericArray<Unit>();
        }
    }
}
