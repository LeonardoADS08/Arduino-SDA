using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDA_Core.Business;
using SDA_Core.Functional;

namespace SDA_Core.Data
{
    static public class MeasureUnitSerializer 
    {
        static private Functional.DataSerializer<Business.MeasureUnit> _serializer;

        public static DataSerializer<MeasureUnit> Serializer { get => _serializer; set => _serializer = value; }

        static MeasureUnitSerializer()
        {
            _serializer = new DataSerializer<MeasureUnit>();
        }
    }
}
