using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDA_Core.Business.Arrays;
using SDA_Core.Functional;

namespace SDA_Core.Data
{
    public static class SensorDataSerializer 
    {
        static private Functional.DataSerializer<Business.Arrays.SensorData> _serializer;

        static public DataSerializer<SensorData> Serializer { get => _serializer; set => _serializer = value; }

        static SensorDataSerializer()
        {
            _serializer = new DataSerializer<SensorData>();
        }

    }
}
