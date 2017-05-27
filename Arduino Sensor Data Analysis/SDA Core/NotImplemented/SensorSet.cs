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
        private Processing _processing;

        /// <summary>
        /// ES: Se crea por defecto la primera columna que será el tiempo: 'Time'.
        /// </summary>
        public SensorSet()
        {
            NewColumn("Time");
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
        /// <param name="list">ES: Una lista con todos los nombre de las columnas que se desea que tenga el SensorSet.</param>
        public SensorSet(string setName, params string[] list)
        {
            NewColumn("Time");
            for (int i = 0; i < list.Length; ++i)
                NewColumn(list[i]);
        }

    }
}
