using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SDA_Core.Functional
{
    public class Data
    {
        public DataTable BaudRatesListToDataTable(Business.Arrays.BaudRatesArray data)
        {
            DataTable result = new DataTable();

            result.Columns.Add("BaudRate", typeof(int));

            for (int i = 0; i < data.List.Size; ++i)
            {
                Business.BaudRates baudRate = data.List[i];
                DataRow newRow = result.NewRow();

                newRow[0] = baudRate.Value;

                result.Rows.Add(newRow);
            }
            return result;
        }

        public DataTable SensorsListToDataTable(SDA_Core.Business.Arrays.SensorDataArray data)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Name", typeof(string));
            result.Columns.Add("Column count", typeof(int));

            for (int i = 0; i < data.List.Size; ++i)
            {
                Business.Arrays.SensorData sensor = data.List[i];
                DataRow newRow = result.NewRow();

                newRow[0] = sensor.SensorName;
                newRow[1] = sensor.Columns.Size;

                result.Rows.Add(newRow);
            }
            return result;
        }

        public DataTable UnitListToDataTable(Business.Arrays.UnitArray data)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Unit", typeof(string));

            for (int i = 0; i < data.List.Size; ++i)
            {
                Business.Unit unit = data.List[i];
                DataRow newRow = result.NewRow();

                newRow[0] = unit.Name;

                result.Rows.Add(newRow);
            }
            return result;
        }

        public DataTable MeasureListToDataTable(Business.Arrays.MeasureArray data)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Measure", typeof(string));

            for (int i = 0; i < data.List.Size; ++i)
            {
                Business.Measure measure = data.List[i];
                DataRow newRow = result.NewRow();

                newRow[0] = measure.Name;

                result.Rows.Add(newRow);
            }
            return result;
        }

        public DataTable MeasureUnitListToDataTable(Business.Arrays.MeasureUnitArray data)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Measure", typeof(string));
            result.Columns.Add("Unit", typeof(string));

            for (int i = 0; i < data.List.Size; ++i)
            {
                Business.MeasureUnit measure = data.List[i];
                DataRow newRow = result.NewRow();

                newRow[0] = measure.Measure.Name;
                newRow[1] = measure.Unit.Name;

                result.Rows.Add(newRow);
            }
            return result;
        }

        public DataTable SensorDataToDataTable(Business.Arrays.SensorData data)
        {
            DataTable result = new DataTable();

            for (int i = 0; i < data.Columns.Size; ++i)
            {
                result.Columns.Add(data.Columns[i].Measure.Name + " (" + data.Columns[i].Unit.Name + ")", typeof(double));
            }

            for (int i = 0; i < data.Rows; ++i)
            {
                DataRow newRow = result.NewRow();

                for (int j = 0; j < data.Columns.Size; ++j)
                {
                    Business.Measurement actual = data.Columns[j].List[i];
                    newRow[j] = actual.Value;
                }

                result.Rows.Add(newRow);
            }

            return result;
        }
    }
}
