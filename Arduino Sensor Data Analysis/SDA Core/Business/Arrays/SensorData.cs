using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business.Arrays
{
    [Serializable]
    public class SensorData
    {
        private GenericArray<MeasurementArray> _columns;
        private string _sensorName;
        private int _rows;

        public GenericArray<MeasurementArray> Columns { get => _columns; set => _columns = value; }
        public string SensorName { get => _sensorName; set => _sensorName = value; }
        public int Rows { get => _rows; set => _rows = value; }

        public SensorData()
        {
            _columns = new GenericArray<MeasurementArray>();
            _sensorName = "";
            _rows = 0;
        }

        public void AddRow(GenericArray<Measurement> row)
        {
            for (int i = 0; i < _columns.Size; ++i)
            {
                _columns[i].List.Add(row[i]);
            }
            _rows++;
        }
    }
}
