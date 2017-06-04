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
        public void Process(string rawData, ref Containers.SensorData resultTable, List<Containers.Measurement> format)
        {
            // Se verifica que el mensaje sea para SDA.
            if (rawData.StartsWith("SDA: ")) rawData = rawData.Remove(0, 5);
            else return;

            // Se separan los datos
            string[] rawColumnsData = rawData.Split(' ');
            List<Containers.Measurement> receivedData = new List<Containers.Measurement>();

            // Se convierten los datos a double
            for (int i = 0; i < rawColumnsData.Length; ++i)
            {
                try
                {
                    double processedData = Convert.ToDouble(rawColumnsData[i]);
                    Containers.Measurement newMeasure = new Containers.Measurement();

                    newMeasure.Value = processedData;
                    newMeasure.Measure = format[i].Measure;
                    newMeasure.Name = format[i].Name;
                    receivedData.Add(newMeasure);
                }
                catch
                {
                    RuntimeLogs.SendLog("Imposible convertir: " + rawColumnsData[i], "Processing.Process(string, ref SensorDataArray, bool = false)");
                }
                
            }
            resultTable.Add(receivedData);
        }

        /// <summary>
        /// ES: Procesa datos recogidos por Arduino SDA que están almacenados en una lista.
        /// </summary>
        /// <param name="rawData">ES: Lista de strings con los datos sin procesar. </param>
        /// <param name="resultTable">ES: SensorDataArray donde se almacenaran los resultados</param>
        /// <param name="clearTable">ES: Indicar 'true' si se quiere limpiar los datos de la tabla</param>
        public void Process(List<String> rawData, ref Containers.SensorData resultTable, List<Containers.Measurement> format, bool clearTable = false)
        {
            if (clearTable) resultTable.Clear();
            foreach (string data in rawData)
            {
                Process(data, ref resultTable, format);
            }
        }


    }
}
