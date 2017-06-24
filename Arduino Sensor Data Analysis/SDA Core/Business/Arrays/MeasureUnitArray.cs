using System;

namespace SDA_Core.Business.Arrays
{
    [Serializable]
    public class MeasureUnitArray : MeasureUnit
    {
        private GenericArray<MeasureUnit> _list;

        public GenericArray<MeasureUnit> List { get => _list; set => _list = value; }

        public MeasureUnitArray()
        {
            _list = new GenericArray<MeasureUnit>();
        }
    }
}