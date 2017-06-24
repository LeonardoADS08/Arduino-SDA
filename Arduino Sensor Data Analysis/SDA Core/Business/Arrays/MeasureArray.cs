using System;

namespace SDA_Core.Business.Arrays
{
    [Serializable]
    public class MeasureArray : Measure
    {
        private GenericArray<Measure> _list;

        public GenericArray<Measure> List { get => _list; set => _list = value; }

        public MeasureArray()
        {
            _list = new GenericArray<Measure>();
        }
    }
}