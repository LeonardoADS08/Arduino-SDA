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
    class MeasureUnitInterface
    {
        SDA_Core.Business.Arrays.MeasureUnitArray measureUnitArray;
        SDA_Core.Functional.Data dataManager;

        public MeasureUnitInterface()
        {
            measureUnitArray = new SDA_Core.Business.Arrays.MeasureUnitArray();
            dataManager = new SDA_Core.Functional.Data();
        }

        public void LoadUnits(ComboBox CB_Units, SDA_Core.Business.Arrays.UnitArray data)
        {
            CB_Units.Items.Clear();
            CB_Units.SelectedValuePath = "Name";
            CB_Units.DisplayMemberPath = "Name";
            for (int i = 0; i < data.List.Size; ++i)
            {
                CB_Units.Items.Add(data.List[i]);
            }
        }

        public void LoadMeasures(ComboBox CB_Measure, SDA_Core.Business.Arrays.MeasureArray data)
        {
            CB_Measure.Items.Clear();
            CB_Measure.SelectedValuePath = "Name";
            CB_Measure.DisplayMemberPath = "Name";
            for (int i = 0; i < data.List.Size; ++i)
            {
                CB_Measure.Items.Add(data.List[i]);
            }
        }

        public void UpdateTable(DataGrid DG_MeasuresUnits) => DG_MeasuresUnits.ItemsSource = dataManager.MeasureUnitListToDataTable(measureUnitArray).AsDataView();

        public void NewMeasureUnite(DataGrid DG_MeasuresUnits, ComboBox CB_Measure, ComboBox CB_Unit)
        {
            if (CB_Measure.SelectedItem == null || CB_Unit.SelectedItem == null)
            {
                MessageBox.Show("Empty fields found.", "Error", MessageBoxButton.OK);
                return;
            }

            SDA_Core.Business.Measure selectedMeasure = (SDA_Core.Business.Measure)CB_Measure.SelectedItem;
            SDA_Core.Business.Unit selectedUnit = (SDA_Core.Business.Unit)CB_Unit.SelectedItem;

            SDA_Core.Business.MeasureUnit newMeasureUnit = new SDA_Core.Business.MeasureUnit(selectedMeasure, selectedUnit);

            CB_Measure.SelectedIndex = -1;
            CB_Unit.SelectedIndex = -1;

            measureUnitArray.List.Add(newMeasureUnit);
            UpdateTable(DG_MeasuresUnits);
        }

        public void DeleteMeasureUnit(DataGrid DG_MeasuresUnits)
        {
            if (DG_MeasuresUnits.SelectedItem == null) return;
            int selected = DG_MeasuresUnits.SelectedIndex;
            measureUnitArray.List.Remove(selected);
            UpdateTable(DG_MeasuresUnits);
        }

        public void LoadDataGrid(DataGrid DG_MeasuresUnits, SDA_Core.Business.Arrays.MeasureUnitArray data)
        {
            measureUnitArray = data;
            UpdateTable(DG_MeasuresUnits);
        }

        public SDA_Core.Business.Arrays.MeasureUnitArray GetData() => measureUnitArray;
    }
}
