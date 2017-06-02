using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data.Containers
{
    public class Data
    {
        private List<List<double>> _information;

        public List<List<double>> Information
        {
            get { return _information; }
            set { _information = value; }
        }

        /// <summary>
        /// ES: Constructor de la clase Data.
        /// </summary>
        public Data()
        {
            _information = new List<List<double>>();
        }
    }
}
