using System;

namespace SDA_Core.Business.Arrays
{
    [Serializable]
    public class SensorDataArray
    {
        private GenericArray<SensorData> _list;

        public GenericArray<SensorData> List { get => _list; set => _list = value; }

        public SensorDataArray()
        {
            _list = new GenericArray<SensorData>();
        }
    }
}