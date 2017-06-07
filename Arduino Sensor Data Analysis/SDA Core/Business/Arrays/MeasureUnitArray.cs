using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business.Arrays
{
    [Serializable]
    public class MeasureUnitArray
    {
        private GenericArray<MeasureUnit> _list;

        public GenericArray<MeasureUnit> List { get => _list; set => _list = value; }

        public MeasureUnitArray()
        {
            _list = new GenericArray<MeasureUnit>();
        }
    }
}
