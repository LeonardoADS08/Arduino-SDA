using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Data;
using System.IO.Ports;
using System.Diagnostics;
using SDA_Core;

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
        // Comunicación
        private DataTable SensorDataDataTableFormat(Business.Arrays.SensorData data)
        {
            DataTable result = new DataTable();

            for (int i = 0; i < data.Columns.Size; ++i)
            {
                string columnName = data.Columns[i].Measure.Name + " (" + data.Columns[i].Unit.Name + ")";
                int tr = 1;
                while (result.Columns.Contains(columnName))
                {
                    columnName = data.Columns[i].Measure.Name + tr.ToString() + " (" + data.Columns[i].Unit.Name + ")";
                    tr++;
                }
                result.Columns.Add(columnName, typeof(double));
            }
            return result;
        }

        public void SensorDataToDataTable(ref DataTable data, Business.Arrays.SensorData information)
        {
            int init = data.Rows.Count, last = information.Rows;

            if (data.Columns.Count == 0) data = SensorDataDataTableFormat(information);


            for (int i = init; i < last; ++i)
            {
                DataRow newRow = data.NewRow();
                for (int j = 0; j < information.Columns.Size; ++j)
                {
                    try { newRow[j] = information.Columns[j].List[i].Value; }
                    catch { break; }
                }
                data.Rows.Add(newRow);
            }
            
        }
    }
}
