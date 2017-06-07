using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business
{
    [Serializable]
    public class Measurement
    {
        private MeasureUnit _measureInformation;
        private double _value;

        public MeasureUnit MeasureInformation { get => _measureInformation; set => _measureInformation = value; }
        public double Value { get => _value; set => _value = value; }

        public Measurement()
        {
            _measureInformation = new MeasureUnit();
            _value = 0;
        }

        public Measurement(MeasureUnit measureInfo)
        {
            _measureInformation = measureInfo;
        }

        public Measurement(MeasureUnit measureInfo, double value)
        {
            _measureInformation = measureInfo;
            _value = value;
        }
    }
}
