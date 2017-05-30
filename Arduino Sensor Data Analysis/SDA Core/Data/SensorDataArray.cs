using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data
{
    public class SensorDataArray
    {
        private GenericArray<string> _columns;
        private GenericArray<string> _measures;
        private GenericArray<SensorData> _rows;

        /// <summary>
        /// ES: Constructor de la clase SensorDataArray.
        /// </summary>
        public SensorDataArray()
        {
            _columns = new GenericArray<string>();
            _measures = new GenericArray<string>();
            _rows = new GenericArray<SensorData>();
        }

        /// <summary>
        /// ES: Constructor de la clase SensorDataArray, iniciando la cantidad y nombre de las columnas y sus medidas.
        /// </summary>
        /// <param name="columns">ES: Lista de columnas.</param>
        /// <param name="measures">ES: Lista de medidas.</param>
        public SensorDataArray(GenericArray<string> columns, GenericArray<string> measures)
        {
            _columns = columns;
            _measures = measures;
            _rows = new GenericArray<SensorData>();
        }

        /// <summary>
        /// ES: Devuelve la cantidad de columnas que se hayan registrado.
        /// </summary>
        /// <returns>ES: Cantidad de columnas.</returns>
        public int ColumnCount()
        {
            return _columns.Size;
        }

        /// <summary>
        /// ES: Registra una nueva columna.
        /// </summary>
        /// <param name="column">ES: Nombre de la columna.</param>
        public void NewColumn(string column, string measure)
        {
            _columns.Add(column);
        }

        /// <summary>
        /// ES: Busca el nombre de una columna.
        /// </summary>
        /// <param name="pos">ES: Posición de la columna.</param>
        /// <returns>ES: Nombre de la columna.</returns>
        public string Columns(int pos)
        {
            return _columns.Data[pos];
        }

        /// <summary>
        /// ES: Devuelve todas las columnas en un GenericArray<string>.
        /// </summary>
        /// <returns>ES: Columnas.</returns>
        public GenericArray<string> Columns()
        {
            return _columns;
        }

        /// <summary>
        /// ES: Devuelve la medida que se registro para una columna.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public string Measure(int pos)
        {
            return _measures.Data[pos];
        }

        /// <summary>
        /// ES: Devuelve todas las mediciones en un GenericArray<string>.
        /// </summary>
        /// <returns>ES: Columnas.</returns>
        public GenericArray<string> Measures(int pos)
        {
            return _measures;
        }

        /// <summary>
        /// ES: Devuelve la cantidad de filas que se hayan registrado.
        /// </summary>
        /// <returns>ES: Cantidad de filas.</returns>
        public int RowCount()
        {
            return _rows.Size;
        }

        /// <summary>
        /// ES: Registra una nueva fila.
        /// </summary>
        /// <param name="row">ES: Fila con sus datos.</param>
        public void NewRow(SensorData row)
        {
            _rows.Add(row);
        }

        /// <summary>
        /// ES: Devuelve una fila en un objeto SensorData.
        /// </summary>
        /// <param name="pos">ES: Posicion de la fila a devolver.</param>
        /// <returns>ES: Fila con los respectivos datos.</returns>
        public SensorData Row(int pos)
        {
            return _rows.Data[pos];
        }

        /// <summary>
        /// ES: Elimina todos los datos del SensorDataArray, mantiene las columnas.
        /// </summary>
        public void Clear()
        {
            _rows.Clear();
        }
    }
}
