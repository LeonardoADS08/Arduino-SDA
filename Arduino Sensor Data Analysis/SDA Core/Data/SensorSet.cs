using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SDA_Core.Data
{
    /// <summary>
    /// ES: Clase para almacenar sensores y los datos de de su conjunto.
    /// </summary>
    [Serializable]
    public class SensorSet : Set<double>
    {
        /// <summary>
        /// ES: Nombre del conjunto de sensores.
        /// </summary>
        public string SensorSetName
        {
            get { return SetName; }
            set { SetName = value; }
        }

        /// <summary>
        /// ES: Se crea por defecto la primera columna que será el tiempo: 'Time'.
        /// </summary>
        /// <param name="setName">ES: Nombre del SensorSet.</param>
        public SensorSet(string setName)
        {
            NewColumn("Time");
        }

        /// <summary>
        /// ES: Permite iniciar un SensorSet con varias columnas al iniciar, la primera columna sigue siendo 'Time'.
        /// </summary>
        /// <param name="setName">ES: Nombre del SensorSet.</param>
        public SensorSet(string setName, params string[] list)
        {
            for (int i = 0; i < list.Length; ++i)
                NewColumn(list[i]);
        }

    }
}
