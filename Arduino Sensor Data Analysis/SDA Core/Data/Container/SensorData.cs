using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SDA_Core.Data.Containers
{
    /// <summary>
    /// ES: Clase que funciona para almacenar datos recibidos en un formato de tabla.
    /// </summary>
    public class SensorData
    {

        private Sensor _headers;
        private Data _data;

        /// <summary>
        /// ES: Cabeceras del SensorData, con sus respectivos nombres y medidas.
        /// </summary>
        public Sensor Headers
        {
            get { return _headers; }
            set { _headers = value; }
        }

        /// <summary>
        /// ES: Datos del SensorData, separados por filas y columnas.
        /// </summary>
        public Data Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// ES: Constructor de la clase SensorData.
        /// </summary>
        public SensorData()
        {
            _headers = new Sensor();
            _data = new Data();
        }

        /// <summary>
        /// ES: Convierte el objeto en un DataTable.
        /// </summary>
        public DataTable ToDataTable()
        {
            DataTable result = new DataTable();

            // Se crean primero las cabeceras.
            foreach (Measurement header in _headers.MeasureList.Measures)
            {
                result.Columns.Add(header.Name + " (" + header.Measure + ")");
            }

            // Se pasan los datos.
            foreach (List<double> row in _data.Information)
            {
                DataRow resultRow = result.NewRow();
                for (int i = 0; i < row.Count; ++i)
                {
                    resultRow[i] = row[i];
                }
                result.Rows.Add(resultRow);
            }

            return result;
        }
    }
}
