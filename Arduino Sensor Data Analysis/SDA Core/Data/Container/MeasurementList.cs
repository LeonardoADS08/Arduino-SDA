using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data.Containers
{
    /// <summary>
    /// ES: Clase para guardar una lista de mediciones.
    /// </summary>
    public class MeasurementList
    {
        private List<Measurement> _measures;

        public List<Measurement> Measures
        {
            get { return _measures; }
            set { _measures = value; }
        }


        /// <summary>
        /// ES: Constructor de la clase MeasurementList.
        /// </summary>
        public MeasurementList()
        {
            _measures = new List<Measurement>();
        }
    }
}
