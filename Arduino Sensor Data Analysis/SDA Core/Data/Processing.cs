using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDA_Core.Communication;
using System.IO;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Data;

namespace SDA_Core.Data
{
    /// <summary>
    /// ES: Clase encargada de procesar los datos recogidos.
    /// </summary>
    public class Processing
    {
        private USB _usb;
        private Bluetooth _bluetooth;

        /// <summary>
        /// ES: Constructor de la clase Processing.
        /// </summary>
        public Processing()
        {

        }

        /// <summary>
        /// ES: Procesa datos recogidos por Arduino SDA.
        /// </summary>
        /// <param name="rawData">ES: String con los datos sin procesar.</param>
        /// <param name="resultTable">ES: DataTable donde se almacenaran los resultados</param>
        /// <param name="clearTable">ES: Indicar 'true' si se quiere limpiar los datos de la tabla</param>
        public void Process(string rawData, ref SensorSet resultTable, bool clearTable = false)
        {
            if (clearTable) resultTable.Clear();
            DataRow data = resultTable.NewRow();

            // Se separan los datos
            string[] rawColumnsData = rawData.Split(null);

            // Se convierten los datos a Double
            for(int i = 0; i < rawColumnsData.Length; ++i)
            {
                data[i] = Convert.ToDouble(rawColumnsData[i]);
            }

            // Se añade a la tabla
            resultTable.AddData(data);
        }

        /// <summary>
        /// ES: Procesa datos recogidos por Arduino SDA que están almacenados en una lista.
        /// </summary>
        /// <param name="rawData">ES: Lista de strings con los datos sin procesar. </param>
        /// <param name="resultTable">ES: DataTable donde se almacenaran los resultados</param>
        /// <param name="clearTable">ES: Indicar 'true' si se quiere limpiar los datos de la tabla</param>
        public void Process(List<String> rawData, ref SensorSet resultTable, bool clearTable = false)
        {
            if (clearTable) resultTable.Clear();
            foreach (string data in rawData)
            {
                Process(data, ref resultTable, clearTable);
            }
        }

    }
}
