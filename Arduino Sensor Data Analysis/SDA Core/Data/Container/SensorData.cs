using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;

namespace SDA_Core.Data.Containers
{
    /// <summary>
    /// ES: Clase que funciona para almacenar datos recibidos en un formato de tabla.
    /// </summary>
    public class SensorData
    {
        private string _sensorName;
        private List<MeasurementList> _values;
        private int _rows;
        private int _columns;

        public string SensorName
        {
            get { return _sensorName; }
            set { _sensorName = value; }
        }

        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        public int Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public List<MeasurementList> Values
        {
            get { return _values; }
            set { _values = value; }
        }

        /// <summary>
        /// ES: Constructor de la clase SensorData.
        /// </summary>
        public SensorData()
        {
            _values = new List<MeasurementList>();
            _sensorName = "";
            _rows = 0;
            _columns = 0;
        }

        /// <summary>
        /// ES: Constructor de la clase SensorData.
        /// </summary>
        public SensorData(int columns)
        {
            _values = new List<MeasurementList>();
            for (int i = 0; i < columns; ++i)
            {
                _values.Add(new MeasurementList());
            }
            _sensorName = "";
            _rows = 0;
            _columns = columns;
        }

        /// <summary>
        /// ES: Método para añadir una nueva fila.
        /// </summary>
        public void Add(List<Measurement> data)
        {
            for (int i = 0; i < _columns; ++i)
            {
                _values[i].Measures.Add(data[i]);
            }
            _rows++;
        }

        /// <summary>
        /// ES: Elimina todos los datos del SensorData.
        /// </summary>
        public void Clear()
        {
            _values = new List<MeasurementList>();
            for (int i = 0; i < _columns; ++i)
            {
                _values.Add(new MeasurementList());
            }
        }

        /// <summary>
        /// ES: Sobrecarga del operador '[]'.
        /// </summary>
        /// <param name="key">ES: Posición a acceder.</param>
        public List<Measurement> this[int key]
        {
            get
            {
                List<Measurement> result = new List<Measurement>();
                for (int i = 0; i < _columns; ++i)
                {
                    result.Add(_values[i].Measures[key]);
                }
                return result;
            }
            set
            {
                for (int i = 0; i < value.Count; ++i)
                {
                    _values[i].Measures[key] = value[i];
                }
            }
        }

        public DataTable ToDataTable()
        {
            DataTable result = new DataTable();

            if (_rows == 0 || _columns == 0) return result;

            for (int i = 0; i < _columns; ++i)
            {
                result.Columns.Add(_values[i].Measures[0].Name + " (" + _values[i].Measures[0].Measure + ")");
            }
            
            for (int i = 0; i < _rows; ++i)
            {
                DataRow newRow = result.NewRow();
                for (int j = 0; j < _columns; ++j)
                {
                    newRow[j] = _values[j].Measures[i].Value;
                }
                result.Rows.Add(newRow);
            }

            return result;
        }

    }
}
