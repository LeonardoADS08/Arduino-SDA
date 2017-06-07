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
        private string _name;

        public string Name { get => _name; set => _name = value; }

        public Unit()
        {
            _name = "";
        }

        public Unit(string unitName)
        {
            _name = unitName;
        }

        
    }
}
