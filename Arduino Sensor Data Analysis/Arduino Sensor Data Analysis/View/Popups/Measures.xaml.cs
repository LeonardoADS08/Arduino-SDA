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
    /// Lógica de interacción para Measures.xaml
    /// </summary>
    public partial class Measures : Window
    {
        SDA_Program.Interface.MeasuresInterface IO;
        SDA_Program.Application.MeasuresApplication APP;

        public Measures()
        {
            InitializeComponent();
            IO = new Interface.MeasuresInterface();
            APP = new Application.MeasuresApplication();

            IO.LoadDataGrid(DG_Measures, APP);
        }

        private void B_SaveData_Click(object sender, RoutedEventArgs e)
        {
            APP.SaveDataToBinary(IO.GetData());
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            IO.NewUnit(DG_Measures, TB_Measure);
        }

        private void B_Delete_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteBaudRate(DG_Measures);
        }

        private void DG_Measures_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
