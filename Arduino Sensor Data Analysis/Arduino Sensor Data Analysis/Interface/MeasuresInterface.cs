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
    class MeasuresInterface
    {
        SDA_Core.Business.Arrays.MeasureArray measuresArray;
        SDA_Core.Functional.Data dataManager;

        public MeasuresInterface()
        {
            measuresArray = new SDA_Core.Business.Arrays.MeasureArray();
            dataManager = new SDA_Core.Functional.Data();
        }

        public void UpdateTable(DataGrid DG_Measures) => DG_Measures.ItemsSource = dataManager.MeasureListToDataTable(measuresArray).AsDataView();

        public void NewUnit(DataGrid DG_Measures, TextBox TB_Measures)
        {
            if (TB_Measures.Text == "")
            {
                MessageBox.Show("Empty fields.", "Error", MessageBoxButton.OK);
                return;
            }

            SDA_Core.Business.Measure newMeasure = new SDA_Core.Business.Measure(TB_Measures.Text);
            measuresArray.List.Add(newMeasure);

            TB_Measures.Text = "";
            UpdateTable(DG_Measures);
        }

        public void DeleteBaudRate(DataGrid DG_Measures)
        {
            if (DG_Measures.SelectedItem == null) return;
            int selected = DG_Measures.SelectedIndex;
            measuresArray.List.Remove(selected);
            UpdateTable(DG_Measures);
        }

        public void LoadDataGrid(DataGrid DG_Measures, SDA_Core.Business.Arrays.MeasureArray data)
        {
            measuresArray = data;
            UpdateTable(DG_Measures);
        }

        public SDA_Core.Business.Arrays.MeasureArray GetData() => measuresArray;
    }
}
