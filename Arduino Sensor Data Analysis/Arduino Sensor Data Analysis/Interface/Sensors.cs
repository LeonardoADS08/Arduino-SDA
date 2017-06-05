using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Diagnostics;

namespace SDA_Program.Interface
{
    class Sensors
    {
        private SDA_Core.Data.Containers.SensorData _newSensor;
        private List<SDA_Core.Data.Containers.Measurement> _format;
        public Sensors()
        {
            _newSensor = new SDA_Core.Data.Containers.SensorData();
            _format = new List<SDA_Core.Data.Containers.Measurement>();
        }

        public void LoadSensors(DataGrid DG_Sensors)
        {
            DG_Sensors.ItemsSource = SDA_Core.Program.SensorsToDataTable().AsDataView();
        }

        private void LoadNewSensorFields(DataGrid DG_NewSensor)
        {
            DG_NewSensor.ItemsSource = NewSensorToDataTable(_format).AsDataView();
        }

        public void NewSensor(DataGrid DG_Sensors, DataGrid DG_NewSensor, TextBox TB_SensorName)
        {
            if (TB_SensorName.Text == "")
            {
                MessageBox.Show("Sensor name field can't be empty.", "Error", MessageBoxButton.OK);
                return;
            }
            if (SDA_Core.Program.SensorList.SensorList.Any(value => value.SensorName == TB_SensorName.Text))
            {
                MessageBox.Show("Sensor name alredy exists.", "Error", MessageBoxButton.OK);
                return;
            }

            SDA_Core.Data.Containers.SensorData newSensor = new SDA_Core.Data.Containers.SensorData();
            newSensor.SensorName = TB_SensorName.Text;

            foreach (SDA_Core.Data.Containers.Measurement value in _format)
            {
                List<SDA_Core.Data.Containers.Measurement> valueList = new List<SDA_Core.Data.Containers.Measurement>();
                valueList.Add(value);
                newSensor.Add(valueList);
            }

            DG_NewSensor.ItemsSource = null;
            TB_SensorName.Text = "";

            _format.Clear();

            SDA_Core.Program.SensorList.SensorList.Add(newSensor);
            LoadSensors(DG_Sensors);
        }
        public void DeleteSensorField(DataGrid DG_NewSensor)
        {
            if (DG_NewSensor.SelectedItem != null)
            {
                int selected = DG_NewSensor.SelectedIndex;
                _format.RemoveAt(selected);
                LoadNewSensorFields(DG_NewSensor);
            }
        }

        public void NewSensorField(DataGrid DG_NewSensor, TextBox TB_Measure, TextBox TB_Unit)
        {
            SDA_Core.Data.Containers.Measurement newField = new SDA_Core.Data.Containers.Measurement();
            
            if (TB_Measure.Text == "" || TB_Unit.Text == "")
            {
                MessageBox.Show("Empty fields found.", "Error", MessageBoxButton.OK);
                return;
            }

            newField.Name = TB_Measure.Text;
            newField.Measure = TB_Unit.Text;

            TB_Measure.Text = "";
            TB_Unit.Text = "";

            _format.Add(newField);
            LoadNewSensorFields(DG_NewSensor);
        }

        public void DeleteSensor(DataGrid DG_Sensors)
        {
            if (DG_Sensors.SelectedItem != null)
            {
                int selected = DG_Sensors.SelectedIndex;

                SDA_Core.Program.SensorList.SensorList.RemoveAt(selected);
                LoadSensors(DG_Sensors);
            }
        }

        private DataTable NewSensorToDataTable(List<SDA_Core.Data.Containers.Measurement> data)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Measure", typeof(string));
            result.Columns.Add("Unit", typeof(string));

            foreach (SDA_Core.Data.Containers.Measurement value in data)
            {
                DataRow newRow = result.NewRow();

                newRow[0] = value.Name;
                newRow[1] = value.Measure;

                result.Rows.Add(newRow);
            }

            return result;
        }
    }
}
