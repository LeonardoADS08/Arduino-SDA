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

namespace SDA_Program.View.Popups
{
    /// <summary>
    /// Lógica de interacción para Sensors.xaml
    /// </summary>
    public partial class Sensors : Window
    {
        Interface.SensorInterface IO;
        Application.SensorApplication APP;

        public Sensors()
        {
            InitializeComponent();

            IO = new Interface.SensorInterface();
            APP = new Application.SensorApplication();

            IO.LoadMeasurementUnit(CB_MeasureUnits, APP.GetMeasureUnits());
            IO.LoadSensors(DG_Sensors, APP.GetSensors());
        }

        private void B_DeleteSensor_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteSensor(DG_Sensors);
        }

        private void B_DeleteSensorField_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteField(DG_NewSensor);
        }

        private void B_AddField_Click(object sender, RoutedEventArgs e)
        {
            IO.NewField(DG_NewSensor, CB_MeasureUnits);
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            IO.NewSensor(DG_NewSensor, DG_Sensors, TB_SensorName);
        }

        private void B_SaveData_Click(object sender, RoutedEventArgs e)
        {
            APP.SaveDataToBinary(IO.GetData());
        }

        private void DG_Sensors_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void DG_NewSensor_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
