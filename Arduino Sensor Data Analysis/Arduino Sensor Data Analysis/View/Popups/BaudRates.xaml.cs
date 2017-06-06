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
        SDA_Program.Interface.BaudRates IO;
        public BaudRates()
        {
            InitializeComponent();
            IO = new Interface.BaudRates();

            IO.LoadBaudRates(DG_BaudRates);
        }

        private void B_DeleteBaudRate_Click(object sender, RoutedEventArgs e)
        {
            IO.DeleteBaudRate(DG_BaudRates);
        }

        private void B_NewBaudRate_Click(object sender, RoutedEventArgs e)
        {
            IO.NewBaudRate(DG_BaudRates, TB_BaudRate);
        }

        private void DG_BaudRates_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
