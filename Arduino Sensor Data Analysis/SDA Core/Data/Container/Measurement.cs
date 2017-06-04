using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data.Containers
{
    /// <summary>
    /// ES: Clase para guardar el nombre de una medición y su medida.
    /// </summary>
    public class Measurement
    {
        private string _name;
        private string _measure;
        private double _value;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Measure
        {
            get { return _measure; }
            set { _measure = value; }
        }

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// ES: Constructor de la clase Measurement.
        /// </summary>
        public Measurement()
        {
            _name = "";
            _measure = "";
        }


        /// <summary>
        /// ES: Constructor de la clase Measurement.
        /// </summary>
        public Measurement(string name, string measure)
        {
            _name = name;
            _measure = measure;
        }
    }
}
