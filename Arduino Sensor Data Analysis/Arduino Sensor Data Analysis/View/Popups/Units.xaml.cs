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
    /// Lógica de interacción para Units.xaml
    /// </summary>
    public partial class Units : Window
    {
        private SDA_Program.Interface.UnitInterface IO;
        private SDA_Program.Application.UnitApplication APP;

        public Units()
        {
            InitializeComponent();

            IO = new Interface.UnitInterface();
            APP = new Application.UnitApplication();

            IO.LoadDataGrid(DG_Units, APP.GetData());
        }

        private void B_Delete_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteBaudRate(DG_Units);
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            IO.NewUnit(DG_Units, TB_Unit);
        }

        private void B_SaveData_Click(object sender, RoutedEventArgs e)
        {
            APP.SaveDataToBinary(IO.GetData());
        }

        private void DG_Units_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}