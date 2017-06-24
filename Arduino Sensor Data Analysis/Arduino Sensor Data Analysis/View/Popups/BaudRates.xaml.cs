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
    /// Lógica de interacción para BaudRates.xaml
    /// </summary>
    public partial class BaudRates : Window
    {
        private SDA_Program.Interface.BaudRatesInterface IO;
        private SDA_Program.Application.BaudRatesApplication APP;

        public BaudRates()
        {
            InitializeComponent();
            IO = new Interface.BaudRatesInterface();
            APP = new Application.BaudRatesApplication();

            IO.LoadDataGrid(DG_BaudRates, APP.GetData());
        }

        private void B_DeleteBaudRate_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteBaudRate(DG_BaudRates);
        }

        private void B_NewBaudRate_Click(object sender, RoutedEventArgs e)
        {
            IO.NewBaudRate(DG_BaudRates, TB_BaudRate);
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            APP.SaveDataToBinary(IO.GetData());
        }

        private void DG_BaudRates_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}