using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business
{
    [Serializable]
    public class Unit
    {
        private int _idUnit;
        private string _name;

        public string Name { get => _name; set => _name = value; }
        public int IdUnit { get => _idUnit; set => _idUnit = value; }

        public Unit()
        {
            _idUnit = -1;
            _name = "";
        }

        public Unit(string unitName)
        {
            _idUnit = -1;
            _name = unitName;
        }

        
    }
}
