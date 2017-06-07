using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business
{
    [Serializable]
    public class Measure
    {
        private string _name;

        public string Name { get => _name; set => _name = value; }

        public Measure()
        {
            _name = "";
        }

        public Measure(string name)
        {
            _name = name;
        }

    }
}
