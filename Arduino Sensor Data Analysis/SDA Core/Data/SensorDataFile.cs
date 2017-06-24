using System;
using System.Collections.Generic;

namespace SDA_Core.Data
{
    [Serializable]
    internal class SensorDataFile
    {
        private int _id;
        private List<int> _idMeasuresUnits;
        private string _sensorName;
        private bool _deleted;

        public int Id { get => _id; set => _id = value; }
        public List<int> IdMeasuresUnits { get => _idMeasuresUnits; set => _idMeasuresUnits = value; }
        public string SensorName { get => _sensorName; set => _sensorName = value; }
        public bool Deleted { get => _deleted; set => _deleted = value; }

        public SensorDataFile()
        {
            _id = -1;
        }

        public SensorDataFile(Business.Arrays.SensorData data)
        {
            _id = data.IdSensor;
            _sensorName = data.SensorName;
            _deleted = false;
            _idMeasuresUnits = new List<int>();
            for (int i = 0; i < data.Columns.Size; ++i)
            {
                _idMeasuresUnits.Add(data.Columns[i].IdMeasureUnit);
            }
        }
    }
}