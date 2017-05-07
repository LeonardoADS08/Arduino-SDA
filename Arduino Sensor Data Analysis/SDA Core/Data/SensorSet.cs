using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SDA_Core.Data
{
    /// <summary>
    /// ES: Clase para almacenar sensores y los datos de de su conjunto
    /// </summary>
    [Serializable]
    public class SensorSet
    {
        // ES: Los datos se amlacenan en un dataset.
        private DataTable _data;
        // ES: Se guarda el nombre de la tabla donde se guardaran los datos.
        private string _sensorSetName;

        public string SensorSetName
        {
            get { return _sensorSetName; }
            set { _sensorSetName = value; }
        }

        /// <summary>
        /// ES: Inicializa los campos de la clase a sus valores por defecto que es un data set con unica columna: la columna 'Time'.
        /// </summary>
        /// <param name="setName">ES: Nombre del SensorSet</param>
        private void Initialize(string setName)
        {
            _data = new DataTable();
            this.NewColumn("Time");
            _sensorSetName = setName;
        }

        /// <summary>
        /// ES: Se crea por defecto la primera columna que será el tiempo: 'Time'.
        /// </summary>
        /// <param name="setName">ES: Nombre del SensorSet</param>
        public SensorSet(string setName) { Initialize(setName); }

        /// <summary>
        /// ES: Permite iniciar un SensorSet con varias columnas al iniciar, la primera columna sigue siendo 'Time'.
        /// </summary>
        /// <param name="setName">ES: Nombre del SensorSet</param>
        public SensorSet(string setName, params string[] list)
        {
            Initialize(setName);
            for (int i = 0; i < list.Length; ++i)
            {
                _data = new DataTable();
                _data.Columns.Add(list[i], typeof(double));
            }
            
        }

        /// <summary>
        /// ES: Permite añadir una nueva columna para almacenar datos de sensores, siempre estos datos serán 'double'.
        /// </summary>
        /// <param name="name">ES: Nombre de la nueva columna.</param>
        public void NewColumn(string name) { _data.Columns.Add(name, typeof(double)); }

        /// <summary>
        /// ES: Edita el nombre de la columna a partir del nombre.
        /// </summary>
        /// <param name="columnName">ES: Nombre de la columna a editar.</param>
        /// <param name="newName">ES: Nuevo nombre de la columna</param>
        public void EditColumn(string columnName, string newName) { _data.Columns[columnName].ColumnName = newName; }

        /// <summary>
        /// ES: Edita el nombre de la columna a partir de la posicion en la tabla.
        /// </summary>
        /// <param name="columnPosition">ES: Número de la columna que se desea editar. </param>
        /// <param name="newName">ES: Nuevo nombre de la columna</param>
        public void EditColumn(int columnPosition, string newName) { _data.Columns[columnPosition].ColumnName = newName; }

        /// <summary>
        /// ES: Ingresa una nueva fila de datos al set
        /// </summary>
        /// <param name="data">ES: La fila que se añadira al set.</param>
        public void AddData(DataRow data) { _data.Rows.Add(data); }
        
        /// <summary>
        /// ES: Devuelve los datos de una fila.
        /// </summary>
        /// <param name="rowIndex"> ES: El número de la fila que se desea. </param>
        public DataRow GetData(int rowIndex) { return _data.Rows[rowIndex]; }
    }
}
