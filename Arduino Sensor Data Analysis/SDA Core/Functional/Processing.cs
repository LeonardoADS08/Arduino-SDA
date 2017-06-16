using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace SDA_Core.Functional
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

        private void ProcessThread(string rawData, Business.Arrays.SensorData resultTable)
        {
            // Se verifica que el mensaje sea para SDA.
            if (rawData.StartsWith("SDA: ")) rawData = rawData.Remove(0, 5);
            else return;

            // Se separan los datos
            string[] rawColumnsData = rawData.Split(' ');

            Business.Arrays.GenericArray<Business.Measurement> receivedData = new Business.Arrays.GenericArray<Business.Measurement>();
            Business.MeasureUnit measureInfo;
            Business.Measurement newMeasure;

            // Se convierten los datos a double
            for (int i = 0; i < rawColumnsData.Length; ++i)
            {
                try
                {
                    double processedData = Convert.ToDouble(rawColumnsData[i]);

                    measureInfo = new Business.MeasureUnit(resultTable.Columns[i].Measure, resultTable.Columns[i].Unit);
                    newMeasure = new Business.Measurement(measureInfo);

                    newMeasure.Value = processedData;
                    receivedData.Add(newMeasure);
                }
                catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, "Processing.Process(string, ref Business.Arrays.SensorData )"); }
            }

            resultTable.AddRow(receivedData);
        }

        /// <summary>
        /// ES: Procesa datos recogidos por Arduino SDA.
        /// </summary>
        /// <param name="rawData">ES: String con los datos sin procesar.</param>
        /// <param name="resultTable">ES: SensorDataArray donde se almacenaran los resultados</param>
        /// <param name="clearTable">ES: Indicar 'true' si se quiere limpiar los datos de la tabla</param>
        public void Process(string rawData, Business.Arrays.SensorData resultTable)
        {
            ThreadStart ts = delegate { ProcessThread(rawData, resultTable); };
            new Thread(ts).Start();
        }

    }
}
