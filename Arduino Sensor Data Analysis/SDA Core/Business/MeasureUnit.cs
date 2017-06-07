using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business
{
    [Serializable]
    public class MeasureUnit
    {
        private Measure _measure;
        private Unit _unit;

        public Measure Measure { get => _measure; set => _measure = value; }
        public Unit Unit { get => _unit; set => _unit = value; }

        public MeasureUnit()
        {
            _measure = new Measure();
        }

        public MeasureUnit(Measure measure, Unit unit)
        {
            _measure = measure;
            _unit = unit;
        }
    }
}
