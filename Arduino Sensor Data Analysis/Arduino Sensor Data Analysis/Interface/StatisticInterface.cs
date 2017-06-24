using LiveCharts;
using LiveCharts.Wpf;
using SDA_Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO.Ports;
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

namespace SDA_Program.Interface
{
    public class StatisticInterface
    {
        private SDA_Core.Business.Arrays.SensorData communication;
        private bool calculate = false;
        private int timeInterval = 500;
        private int maxElementsToShow = 10;

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

            result.Columns.Add("Total time (" + communication.Columns[timeColumn].Unit.Name + ")", typeof(double));

            // Calculando tiempo total
            row.Add(Math.Round(communication.Columns[timeColumn].List.Last().Value - communication.Columns[timeColumn].List.First().Value, 4));

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

            // Calculos para mostrar cuantos elementos seran mostrados.
            int init = 0;
            if (data.Rows > maxElementsToShow) init = data.Rows - maxElementsToShow;

            ChartValues<double> series = new ChartValues<double>();
            for (int i = init; i < data.Rows; ++i)
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
            C_General.Series = SeriesCollection;

            UpdateChart(C_General, data, dataColumn);
        }

        private async void UpdateChart(CartesianChart C_General, SDA_Core.Business.Arrays.SensorData data, int dataColumn)
        {
            // Para asegurarse que otras llamadas de UpdateChart sean terminadas
            calculate = false;
            await Task.Delay(1500);
            calculate = true;

            int position = C_General.Series[0].Values.Count - 1;
            double newValue = -1;

            while (calculate)
            {
                try
                {
                    if (data.Rows - 1 >= position)
                    {
                        newValue = data.Columns[dataColumn].List[position].Value;
                        C_General.Series[0].Values.Add(newValue);
                        position++;
                    }
                }
                catch
                {
                    await Task.Delay(timeInterval);
                    continue;
                }

                // Solo se muestran 'maxElementsToShow' valores en la gráfica
                if (C_General.Series[0].Values.Count > maxElementsToShow)
                {
                    try { C_General.Series[0].Values.RemoveAt(0); }
                    catch { break; }
                }

                await Task.Delay(timeInterval);
            }
        }
    }
}