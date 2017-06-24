using System;

namespace SDA_Core.Data
{
    [Serializable]
    internal class MeasuresUnitFile
    {
        private int _id;
        private int _idMeasure;
        private int _idUnit;
        private bool _deleted;

        public int Id { get => _id; set => _id = value; }
        public int IdMeasure { get => _idMeasure; set => _idMeasure = value; }
        public int IdUnit { get => _idUnit; set => _idUnit = value; }
        public bool Deleted { get => _deleted; set => _deleted = value; }

        public MeasuresUnitFile()
        {
            _id = -1;
            _idMeasure = -1;
            _idUnit = -1;
        }

        public MeasuresUnitFile(Business.MeasureUnit value)
        {
            _id = value.IdMeasureUnit;
            _idMeasure = value.Measure.IdMeasure;
            _idUnit = value.Unit.IdUnit;
            _deleted = false;
        }
    }
}