using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data
{
    public class SensorData
    {
        private GenericArray<double> _values;

        public SensorData()
        {
            _values = new GenericArray<double>();
        }

        public double Values(int pos)
        {
            return _values.Data[pos];
        }

        public void Values(int pos, double val)
        {
            _values.Data[pos] = val;
        }

    }
}
