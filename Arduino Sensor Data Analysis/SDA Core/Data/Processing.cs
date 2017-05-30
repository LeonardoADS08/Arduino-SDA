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
using System.Diagnostics;
using System.Globalization;

namespace SDA_Core.Data
{
    /// <summary>
    /// ES: Clase encargada de procesar los datos recogidos.
    /// </summary>
    public class Processing
    {

        /// <summary>
        /// ES: Constructor de la clase Processing.
        /// </summary>
        public Processing() { }

        /// <summary>
        /// ES: Procesa datos recogidos por Arduino SDA.
        /// </summary>
        /// <param name="rawData">ES: String con los datos sin procesar.</param>
        /// <param name="resultTable">ES: SensorDataArray donde se almacenaran los resultados</param>
        /// <param name="clearTable">ES: Indicar 'true' si se quiere limpiar los datos de la tabla</param>
        public void Process(string rawData, ref SensorDataArray resultTable, bool clearTable = false)
        {
            if (clearTable) resultTable.Clear();
            if (resultTable.RowCount() == 5000) resultTable.Clear();

            // Se verifica que el mensaje sea para SDA.
            if (rawData.StartsWith("SDA: ")) rawData = rawData.Remove(0, 5);
            else return;

            // Se separan los datos
            string[] rawColumnsData = rawData.Split(' ');
            SensorData data = new SensorData();

            // Se convierten los datos a double
            for (int i = 0; i < rawColumnsData.Length; ++i)
            {
                double aux = Convert.ToDouble(rawColumnsData[i]);
                data.Values(i, aux);
            }

            resultTable.NewRow(data);
        }

        /// <summary>
        /// ES: Procesa datos recogidos por Arduino SDA que están almacenados en una lista.
        /// </summary>
        /// <param name="rawData">ES: Lista de strings con los datos sin procesar. </param>
        /// <param name="resultTable">ES: SensorDataArray donde se almacenaran los resultados</param>
        /// <param name="clearTable">ES: Indicar 'true' si se quiere limpiar los datos de la tabla</param>
        public void Process(List<String> rawData, ref SensorDataArray resultTable, bool clearTable = false)
        {
            if (clearTable) resultTable.Clear();
            foreach (string data in rawData)
            {
                Process(data, ref resultTable, clearTable);
            }
        }


    }
}
