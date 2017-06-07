using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDA_Core.Business;
using SDA_Core.Functional;

namespace SDA_Core.Data
{
    public class MeasuresSerializer 
    {
        static private Functional.DataSerializer<Business.Measure> _serializer;

        public static DataSerializer<Measure> Serializer { get => _serializer; set => _serializer = value; }

        static MeasuresSerializer()
        {
            _serializer = new DataSerializer<Measure>();
        }

    }
}
