using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDA_Core.Business;
using SDA_Core.Functional;

namespace SDA_Core.Data
{
    public static class UnitSerializer 
    {
        static private Functional.DataSerializer<Business.Unit> _serializer;

        public static DataSerializer<Business.Unit> Serializer { get => _serializer; set => _serializer = value; }

        static UnitSerializer()
        {
            _serializer = new DataSerializer<Business.Unit>();
        }

    }
}
