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

namespace SDA_Program.View
{
    /// <summary>
    /// Lógica de interacción para Configurations.xaml
    /// </summary>
    public partial class Configurations : Page
    {
        private Interface.ConfigurationsInterface IO;
        private Application.ConfigurationsApplication APP;

        public Configurations()
        {
            InitializeComponent();

            IO = new Interface.ConfigurationsInterface();
            APP = new Application.ConfigurationsApplication();

            ReloadData(null, null);
        }

        private void B_Units_Click(object sender, RoutedEventArgs e)
        {
            SDA_Program.View.Popups.Units popup = new Popups.Units();
            popup.Show();
            popup.Closed += new EventHandler(ReloadData);
        }

        private void B_MeasureUnits_Click(object sender, RoutedEventArgs e)
        {
            SDA_Program.View.Popups.MeasureUnit popup = new Popups.MeasureUnit();
            popup.Show();
            popup.Closed += new EventHandler(ReloadData);
        }

        private void B_Measures_Click(object sender, RoutedEventArgs e)
        {
            SDA_Program.View.Popups.Measures popup = new Popups.Measures();
            popup.Show();
            popup.Closed += new EventHandler(ReloadData);
        }

        private void B_BaudRates_Click(object sender, RoutedEventArgs e)
        {
            SDA_Program.View.Popups.BaudRates popup = new Popups.BaudRates();
            popup.Show();
            popup.Closed += new EventHandler(ReloadData);
        }

        private void B_Sensors_Click(object sender, RoutedEventArgs e)
        {
            SDA_Program.View.Popups.Sensors popup = new Popups.Sensors();
            popup.Show();
            popup.Closed += new EventHandler(ReloadData);
        }

        private void ReloadData(object sender, EventArgs e)
        {
            IO.LoadMeasures(DG_Measures, APP.GetMeasures());
            IO.LoadUnits(DG_Units, APP.GetUnits());
            IO.LoadMeasureUnits(DG_MeasuresUnits, APP.GetMeasureUnits());
            IO.LoadBaudRates(DG_BaudRates, APP.GetBaudRates());
            IO.LoadSensors(DG_Sensors, APP.GetSensors());
        }
    }
}