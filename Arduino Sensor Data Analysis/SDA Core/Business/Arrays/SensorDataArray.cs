using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business.Arrays
{
    [Serializable]
    public class SensorDataArray : SensorData
    {
        private GenericArray<SensorData> _list;

        public GenericArray<SensorData> List { get => _list; set => _list = value; }

        public SensorDataArray()
        {
            _list = new GenericArray<SensorData>();
        }
    }
}
