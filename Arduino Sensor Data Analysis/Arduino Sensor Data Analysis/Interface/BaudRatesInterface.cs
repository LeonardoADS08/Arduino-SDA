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
using System.Data;
using SDA_Core;

namespace SDA_Program.Interface
{
    class BaudRatesInterface
    {
        SDA_Core.Business.Arrays.BaudRatesArray baudRates;
        SDA_Core.Functional.Data dataManager;

        public BaudRatesInterface()
        {
            baudRates = new SDA_Core.Business.Arrays.BaudRatesArray();
            dataManager = new SDA_Core.Functional.Data();
        }

        public void UpdateTable(DataGrid DG_BaudRates) => DG_BaudRates.ItemsSource = dataManager.BaudRatesListToDataTable(baudRates).AsDataView();

        public void NewBaudRate(DataGrid DG_BaudRates, TextBox TB_BaudRate)
        {
            if (TB_BaudRate.Text == "")
            {
                MessageBox.Show("Empty fields.", "Error", MessageBoxButton.OK);
                return;
            }
            int baudRateValue;
            if (!Int32.TryParse(TB_BaudRate.Text, out baudRateValue))
            {
                MessageBox.Show("Can't convert '" + TB_BaudRate.Text + "' to integer.", "Error", MessageBoxButton.OK);
                return;
            }

            SDA_Core.Business.BaudRates newBaudRate = new SDA_Core.Business.BaudRates(baudRateValue);
            baudRates.List.Add(newBaudRate);

            TB_BaudRate.Text = "";
            UpdateTable(DG_BaudRates);
        }

        public void DeleteBaudRate(DataGrid DG_BaudRates)
        {
            if (DG_BaudRates.SelectedItem == null) return;
            int selected = DG_BaudRates.SelectedIndex;
            baudRates.List.Remove(selected);
            UpdateTable(DG_BaudRates);
        }

        public void LoadDataGrid(DataGrid DG_BaudRates, SDA_Core.Business.Arrays.BaudRatesArray data)
        {
            baudRates = data;
            UpdateTable(DG_BaudRates);
        }

        public SDA_Core.Business.Arrays.BaudRatesArray GetData() => baudRates;

    }
}
