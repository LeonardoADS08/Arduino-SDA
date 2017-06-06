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
        SDA_Program.Interface.Sensors IO;
        public Sensors()
        {
            InitializeComponent();
            IO = new Interface.Sensors();

            IO.LoadSensors(DG_Sensors);
        }

        private void B_AddField_Click(object sender, RoutedEventArgs e)
        {
            IO.NewSensorField(DG_NewSensor, TB_Measure, TB_Unit);
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            IO.NewSensor(DG_Sensors, DG_NewSensor, TB_SensorName);
        }

        private void B_DeleteSensor_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteSensor(DG_Sensors);
        }
        private void B_DeleteSensorField_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteSensorField(DG_NewSensor);
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
