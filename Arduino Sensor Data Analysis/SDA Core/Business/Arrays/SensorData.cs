using System;

namespace SDA_Core.Business.Arrays
{
    [Serializable]
    public class SensorData
    {
        private int _idSensor;
        private GenericArray<MeasurementArray> _columns;
        private string _sensorName;
        private int _rows;

        public int IdSensor { get => _idSensor; set => _idSensor = value; }
        public GenericArray<MeasurementArray> Columns { get => _columns; set => _columns = value; }
        public string SensorName { get => _sensorName; set => _sensorName = value; }
        public int Rows { get => _rows; set => _rows = value; }

        public SensorData()
        {
            _idSensor = -1;
            _columns = new GenericArray<MeasurementArray>();
            _sensorName = "";
            _rows = 0;
        }

        public void AddRow(GenericArray<Measurement> row)
        {
            if (row.Size != _columns.Size) return;
            for (int i = 0; i < _columns.Size; ++i)
            {
                try
                {
                    _columns[i].List.Add(row[i]);
                }
                catch (Exception ex)
                {
                    Functional.RuntimeLogs.SendLog(ex.Message, "SensorData.AddRow(GenericArray<Measurement>)");
                    return;
                }
            }
            _rows++;
        }

        public void AddColumn(MeasureUnit column)
        {
            MeasurementArray newColumn = new MeasurementArray();
            newColumn.Measure = column.Measure;
            newColumn.Unit = column.Unit;
            _columns.Add(newColumn);
        }
    }
}