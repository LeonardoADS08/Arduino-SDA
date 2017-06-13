using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.IO.Ports;
using System.Diagnostics;
using SDA_Core;
using LiveCharts;
using LiveCharts.Wpf;

namespace SDA_Program.Interface
{
    public class StatisticInterface
    {
        SDA_Core.Business.Arrays.SensorData communication;
        bool calculate = false;
        int chart = 0;

        public void LoadColumns(ComboBox CB_Columns, SDA_Core.Business.Arrays.SensorData data)
        {
            communication = data;
            CB_Columns.Items.Clear();
            CB_Columns.DisplayMemberPath = "Measure.Name";
            CB_Columns.SelectedValuePath = "Measure.Name";
            for (int i = 0; i < data.Columns.Size; ++i)
            {
                SDA_Core.Business.MeasureUnit unit = new SDA_Core.Business.MeasureUnit(data.Columns[i].Measure, data.Columns[i].Unit);
                CB_Columns.Items.Add(unit);
            }
        }

        public void LoadStatistic(DataGrid DG_Statistic, ComboBox CB_Columns)
        {
            List<double> row = new List<double>();
            // Indica la columna de tiempo
            if (CB_Columns.SelectedItem == null)
            {
                MessageBox.Show("Time column can't be empty.", "Error", MessageBoxButton.OK);
                return;
            }

            int timeColumn = CB_Columns.SelectedIndex;

            // Añade las columnas
            DataTable result = new DataTable();

            result.Columns.Add("Total time (" + communication.Columns[timeColumn].Unit.Name + ")", typeof(double) );
           
            // Calculando tiempo total
            row.Add(communication.Columns[timeColumn].List.Last().Value - communication.Columns[timeColumn].List.First().Value);

            // Prom
            for (int i = 0; i < communication.Columns.Size; ++i)
            {
                if (i == timeColumn) continue;

                string columnName = "~" + communication.Columns[i].Measure.Name + " (" + communication.Columns[i].Unit.Name + ")";
                int tr = 1;
                while (result.Columns.Contains(columnName))
                {
                    columnName = "~" + communication.Columns[i].Measure.Name + tr.ToString() + " (" + communication.Columns[i].Unit.Name + ")";
                    tr++;
                }
                result.Columns.Add(columnName, typeof(double));

                double prom = 0;

                for (int j = 0; j < communication.Rows; ++j)
                {
                    prom += communication.Columns[i].List[j].Value;
                }

                prom /= communication.Rows;
                prom = Math.Round(prom, 4);
                row.Add(prom);
            }

            // Max's & Min's
            for (int i = 0; i < communication.Columns.Size; ++i)
            {
                if (i == timeColumn) continue;

                // Max column
                string columnName = "Max " + communication.Columns[i].Measure.Name + " (" + communication.Columns[i].Unit.Name + ")";
                int tr = 1;
                while (result.Columns.Contains(columnName))
                {
                    columnName = "Max " + communication.Columns[i].Measure.Name + tr.ToString() + " (" + communication.Columns[i].Unit.Name + ")";
                    tr++;
                }
                result.Columns.Add(columnName, typeof(double));

                // Min column
                columnName = "Min " + communication.Columns[i].Measure.Name + " (" + communication.Columns[i].Unit.Name + ")";
                tr = 1;
                while (result.Columns.Contains(columnName))
                {
                    columnName = "Min " + communication.Columns[i].Measure.Name + tr.ToString() + " (" + communication.Columns[i].Unit.Name + ")";
                    tr++;
                }
                result.Columns.Add(columnName, typeof(double));

                double min = 0, max = 0;
                bool first = true;

                for (int j = 0; j < communication.Rows; ++j)
                {
                    if (first)
                    {
                        first = false;
                        min = communication.Columns[i].List[j].Value;
                        max = communication.Columns[i].List[j].Value;
                    }
                    min = (communication.Columns[i].List[j].Value < min) ? communication.Columns[i].List[j].Value : min;
                    max = (communication.Columns[i].List[j].Value > max) ? communication.Columns[i].List[j].Value : max;
                }
                row.Add(max);
                row.Add(min);
            }


            DataRow newRow = result.NewRow();
            for (int i = 0; i < row.Count; ++i)
            {
                newRow[i] = row[i];

            }
            result.Rows.Add(newRow);

            DG_Statistic.ItemsSource = result.AsDataView();
            
        }

        public void GenerateChart(CartesianChart C_General, SDA_Core.Business.Arrays.SensorData data, ComboBox CB_Selected)
        {
            if (CB_Selected.SelectedItem == null) return;
            calculate = false;

            int dataColumn = CB_Selected.SelectedIndex;
            C_General.Series = null;

            ChartValues<double> series = new ChartValues<double>();
            for (int i = 0; i < data.Rows; ++i)
            {
                series.Add(data.Columns[dataColumn].List[i].Value);
            }

            SeriesCollection SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = series
                },
            };
            //C_General.AddToView(SeriesCollection);
            C_General.Series = SeriesCollection;

            UpdateChart(C_General, data, dataColumn);
            
        }

        private async void UpdateChart(CartesianChart C_General, SDA_Core.Business.Arrays.SensorData data, int dataColumn)
        {
            calculate = true;
            int actualChart = chart;
            chart++;
            int init = C_General.Series[0].Values.Count;
            while (calculate)
            {
                double newValue;
                try
                {
                    newValue = data.Columns[dataColumn].List[init].Value;
                }
                catch
                {
                    await Task.Delay(500);
                    continue;
                }
                C_General.Series[0].Values.Add(newValue);
                init++;
                await Task.Delay(250);
            }
        }
    }
}
 