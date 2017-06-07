using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business.Arrays
{
    [Serializable]
    public class MeasureArray
    {
        private GenericArray<Measure> _list;

        public GenericArray<Measure> List { get => _list; set => _list = value; }

        public MeasureArray()
        {
            _list = new GenericArray<Measure>();
        }
    }
}
