using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data
{
    [Serializable]
    class MeasuresFile
    {
        private int _id;
        private string _measure;
        private bool _deleted;

        public int Id { get => _id; set => _id = value; }
        public string Measure { get => _measure; set => _measure = value; }
        public bool Deleted { get => _deleted; set => _deleted = value; }

        public MeasuresFile() { _id = -1; }

        public MeasuresFile(Business.Measure value)
        {
            _id = value.IdMeasure;
            _measure = value.Name;
            _deleted = false;
        }
    }
}
