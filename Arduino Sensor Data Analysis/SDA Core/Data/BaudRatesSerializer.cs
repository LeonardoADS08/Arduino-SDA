using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDA_Core.Business;
using SDA_Core.Functional;

namespace SDA_Core.Data
{
    public static class BaudRatesSerializer
    {
        static private Functional.DataSerializer<Business.BaudRates> _serializer;

        static public DataSerializer<BaudRates> Serializer { get => _serializer; set => _serializer = value; }

        static BaudRatesSerializer()
        {
            _serializer = new DataSerializer<BaudRates>();
        }

    }
}
