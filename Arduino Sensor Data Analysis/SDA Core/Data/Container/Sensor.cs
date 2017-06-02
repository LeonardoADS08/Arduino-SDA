using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data.Containers
{
    /// <summary>
    /// ES: Lista para almacenar sensores.
    /// </summary>
    public class Sensor
    {
        private string _sensorName;
        private MeasurementList _measureList;

        public string SensorName
        {
            get { return _sensorName; }
            set { _sensorName = value; }
        }

        public MeasurementList MeasureList
        {
            get { return _measureList; }
            set { _measureList = value; }
        }

        /// <summary>
        /// ES: Constructor de la clase Sensor.
        /// </summary>
        public Sensor()
        {
            _sensorName = "";
            _measureList = new MeasurementList();
        }
    }
}
