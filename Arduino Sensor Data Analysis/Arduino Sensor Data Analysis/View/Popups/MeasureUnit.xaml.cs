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
    /// Lógica de interacción para MeasureUnit.xaml
    /// </summary>
    public partial class MeasureUnit : Window
    {
        private Interface.MeasureUnitInterface IO;
        private Application.MeasureUnitApplication APP;

        public MeasureUnit()
        {
            InitializeComponent();

            IO = new Interface.MeasureUnitInterface();
            APP = new Application.MeasureUnitApplication();

            IO.LoadDataGrid(DG_MeasureUnit, APP.GetData());

            IO.LoadMeasures(CB_Measure, APP.GetMeasures());
            IO.LoadUnits(CB_Unit, APP.GetUnits());
        }

        private void B_Delete_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteMeasureUnit(DG_MeasureUnit);
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            IO.NewMeasureUnite(DG_MeasureUnit, CB_Measure, CB_Unit);
        }

        private void B_SaveData_Click(object sender, RoutedEventArgs e)
        {
            APP.SaveDataToBinary(IO.GetData());
        }

        private void DG_MeasureUnit_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}