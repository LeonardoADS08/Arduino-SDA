using LiveCharts;
using LiveCharts.Wpf;
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
    /// Lógica de interacción para Statistics.xaml
    /// </summary>
    public partial class Statistics : Page
    {
        private SDA_Core.Business.Arrays.SensorData data;
        private Interface.StatisticInterface IO;

        public Statistics()
        {
            InitializeComponent();

            IO = new Interface.StatisticInterface();
        }

        public void LoadData(SDA_Core.Business.Arrays.SensorData newData)
        {
            data = newData;
            IO.LoadColumns(CB_General_Columns, data);
            IO.LoadColumns(CB_DataColumn, data);
        }

        private void B_Generate_Click(object sender, RoutedEventArgs e)
        {
            IO.LoadStatistic(DG_General, CB_General_Columns);
        }

        private void B_GenerateChart_Click(object sender, RoutedEventArgs e)
        {
            IO.GenerateChart(CC_General, data, CB_DataColumn);
        }
    }
}