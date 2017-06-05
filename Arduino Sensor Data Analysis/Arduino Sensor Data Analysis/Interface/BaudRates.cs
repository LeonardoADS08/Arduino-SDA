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
using System.Data;
using System.Diagnostics;

namespace SDA_Program.Interface
{
    class BaudRates
    {
        public BaudRates()
        {
            
        }

        public void LoadBaudRates(DataGrid DG_BaudRates)
        {
            DG_BaudRates.ItemsSource = SDA_Core.Program.BaudRatesToDataTable().AsDataView();
        }

        public void NewBaudRate(DataGrid DG_BaudRates, TextBox TB_BaudRate)
        {
            int newValue;
            if ( Int32.TryParse(TB_BaudRate.Text, out newValue) )
            {
                SDA_Core.Communication.BaudRates newBaudRate = new SDA_Core.Communication.BaudRates(newValue);
                if ( !SDA_Core.Program.BaudRatesList.BaudRates.Any(value => value.BaudRate == newValue) )
                {
                    SDA_Core.Program.BaudRatesList.BaudRates.Add(newBaudRate);
                    TB_BaudRate.Text = "";
                    LoadBaudRates(DG_BaudRates);
                }
                else
                {
                    MessageBox.Show("Repeated baud rate.", "Error", MessageBoxButton.OK);
                    return;
                }
                
            }
            else
            {
                MessageBox.Show("Can't convert: '" + TB_BaudRate.Text + "' to integer.", "Error", MessageBoxButton.OK);
                return;
            }
        }

        public void DeleteBaudRate(DataGrid DG_BaudRates)
        {
            if (DG_BaudRates.SelectedItem != null)
            {
                int selected = DG_BaudRates.SelectedIndex;
                SDA_Core.Program.BaudRatesList.BaudRates.RemoveAt(selected);
                LoadBaudRates(DG_BaudRates);
            }
        }
    }
}
