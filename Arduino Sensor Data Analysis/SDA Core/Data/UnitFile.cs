using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data
{
    [Serializable]
    class UnitFile
    {
        private int _id;
        private string _unit;
        private bool _deleted;

        public int Id { get => _id; set => _id = value; }
        public string Unit { get => _unit; set => _unit = value; }
        public bool Deleted { get => _deleted; set => _deleted = value; }

        public UnitFile() { _id = -1; }

        public UnitFile(Business.Unit value)
        {
            _id = value.IdUnit;
            _unit = value.Name;
            _deleted = false;
        }
    }
}
