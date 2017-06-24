using System;

namespace SDA_Core.Business.Arrays
{
    [Serializable]
    public class MeasurementArray : MeasureUnit
    {
        private GenericArray<Measurement> _list;

        public GenericArray<Measurement> List { get => _list; set => _list = value; }

        public MeasurementArray()
        {
            _list = new GenericArray<Measurement>();
        }
    }
}