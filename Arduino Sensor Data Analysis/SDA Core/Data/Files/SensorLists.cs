using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDA_Core.Data.Containers;

namespace SDA_Core.Data.Files
{
    public class SensorLists
    {
        List<Containers.SensorData> _sensorList;

        public List<SensorData> SensorList
        {
            get { return _sensorList; }
            set { _sensorList = value; }
        }

        public SensorLists()
        {
            _sensorList = new List<Containers.SensorData>();
        }
    }
}
