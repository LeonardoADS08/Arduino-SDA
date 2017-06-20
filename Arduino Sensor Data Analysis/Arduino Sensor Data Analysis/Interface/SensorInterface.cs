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
using System.Windows.Shapes;
using System.Data;
using SDA_Core;

namespace SDA_Program.Interface
{
    public class SensorInterface
    {
        private SDA_Core.Business.Arrays.SensorDataArray sensors;
        private SDA_Core.Business.Arrays.MeasureUnitArray measureUnitList;
        // Objeto de una clase 'funcional' para hacer transformaciones.
        private SDA_Core.Functional.Data dataManager;
        private SDA_Core.Business.Arrays.MeasureUnitArray newSensorFormat;

        public SensorInterface()
        {
            sensors = new SDA_Core.Business.Arrays.SensorDataArray();
            dataManager = new SDA_Core.Functional.Data();
            newSensorFormat = new SDA_Core.Business.Arrays.MeasureUnitArray();
            measureUnitList = new SDA_Core.Business.Arrays.MeasureUnitArray();
        }

        public void LoadMeasurementUnit(ComboBox CB_MeasurementUnit, SDA_Core.Business.Arrays.MeasureUnitArray data)
        {
            measureUnitList = data;
            CB_MeasurementUnit.Items.Clear();
            for (int i = 0; i < data.List.Size; ++i)
            {
                SDA_Core.Business.MeasureUnit value = data.List[i];
                CB_MeasurementUnit.Items.Add(value.Measure.Name + " (" + value.Unit.Name + ")");
            }
        }

        public void LoadSensors(DataGrid DG_Sensors, SDA_Core.Business.Arrays.SensorDataArray data)
        {
            sensors = data;
            UpdateSensorsTable(DG_Sensors);
        }

        public void DeleteSensor(DataGrid DG_Sensors)
        {
            if (DG_Sensors.SelectedItem == null) return;
            int selected = DG_Sensors.SelectedIndex;
            sensors.List.Remove(selected);
            UpdateSensorsTable(DG_Sensors);
        }

        public void DeleteField(DataGrid DG_NewSensor)
        {
            if (DG_NewSensor.SelectedItem == null) return;
            int selected = DG_NewSensor.SelectedIndex;
            newSensorFormat.List.Remove(selected);
            UpdateNewSensorTable(DG_NewSensor);
        }

        public void NewField(DataGrid DG_NewSensor, ComboBox CB_MeasurementUnit)
        {
            if (CB_MeasurementUnit.SelectedItem == null)
            {
                MessageBox.Show("Empty fields found.", "Error", MessageBoxButton.OK);
                return;
            }

            int selected = CB_MeasurementUnit.SelectedIndex;
            newSensorFormat.List.Add( measureUnitList.List[selected] );

            CB_MeasurementUnit.SelectedIndex = -1;

            UpdateNewSensorTable(DG_NewSensor);
        }

        public void NewSensor(DataGrid DG_NewSensor, DataGrid DG_Sensors, TextBox TB_SensorName)
        {
            if (newSensorFormat.List.Size == 0)
            {
                MessageBox.Show("No MeasureUnit selected for the sensor.", "Error", MessageBoxButton.OK);
                return;
            }
            if (TB_SensorName.Text == "")
            {
                MessageBox.Show("Empty fields found.", "Error", MessageBoxButton.OK);
                return;
            }

            SDA_Core.Business.Arrays.SensorData newSensor = new SDA_Core.Business.Arrays.SensorData();
            newSensor.SensorName = TB_SensorName.Text;

            for (int i = 0; i < newSensorFormat.List.Size; ++i)
            {
                SDA_Core.Business.Arrays.MeasurementArray newMeasurementArray = new SDA_Core.Business.Arrays.MeasurementArray();
                newMeasurementArray.Measure = newSensorFormat.List[i].Measure;
                newMeasurementArray.Unit = newSensorFormat.List[i].Unit;
                newMeasurementArray.IdMeasureUnit = newSensorFormat.List[i].IdMeasureUnit;
                newSensor.Columns.Add(newMeasurementArray);
            }

            newSensorFormat = new SDA_Core.Business.Arrays.MeasureUnitArray();
            sensors.List.Add(newSensor);
            UpdateNewSensorTable(DG_NewSensor);
            UpdateSensorsTable(DG_Sensors);
            
        }

        public SDA_Core.Business.Arrays.SensorDataArray GetData() => sensors;

        public void UpdateSensorsTable(DataGrid DG_Sensors) => DG_Sensors.ItemsSource = dataManager.SensorsListToDataTable(sensors).AsDataView();
        public void UpdateNewSensorTable(DataGrid DG_NewSensor) => DG_NewSensor.ItemsSource = dataManager.MeasureUnitListToDataTable(newSensorFormat).AsDataView();
    }
}
