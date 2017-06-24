using SDA_Core;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace SDA_Program.Interface
{
    public class MeasureUnitInterface
    {
        // Arreglo para almacenar los datos del dataGrid.
        private SDA_Core.Business.Arrays.MeasureUnitArray measureUnitArray;

        // Objeto de una clase 'funcional' para hacer transformaciones.
        private SDA_Core.Functional.Data dataManager;

        /// <summary>
        /// ES: Constructor de la clase MeasureUnitInterface.
        /// </summary>
        public MeasureUnitInterface()
        {
            measureUnitArray = new SDA_Core.Business.Arrays.MeasureUnitArray();
            dataManager = new SDA_Core.Functional.Data();
        }

        /// <summary>
        /// ES: Método para cargar los datos del arreglo 'Unit' a un comboBox.
        /// </summary>
        /// <param name="CB_Units">ES: ComboBox a realizar los cambios.</param>
        /// <param name="data">ES: Datos a cargar.</param>
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

        /// <summary>
        /// ES: Método para cargar los datos del arreglo 'Measures' a un comboBox.
        /// </summary>
        /// <param name="CB_Units">ES: ComboBox a realizar los cambios.</param>
        /// <param name="data">ES: Datos a cargar.</param>
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

        /// <summary>
        /// ES: Método para actualizar la tabla con los nuevos datos que se hayan guardado/eliminado.
        /// </summary>
        /// <param name="DG_MeasuresUnits">ES: DataGrid al que se deban hacer los cambios.</param>
        public void UpdateTable(DataGrid DG_MeasuresUnits) => DG_MeasuresUnits.ItemsSource = dataManager.MeasureUnitListToDataTable(measureUnitArray).AsDataView();

        /// <summary>
        /// ES: Método para generar un nuevo MeasureUnit a partir de los datos ingresados por el usuario.
        /// </summary>
        /// <param name="DG_MeasuresUnits">ES: DataGrid al que se deban hacer los cambios.</param>
        /// <param name="CB_Measure">ES: ComboBox donde se encuentra el objeto 'Measure'.</param>
        /// <param name="CB_Unit">ES: ComboBox donde se encuentra el objeto 'Unit'.</param>
        public void NewMeasureUnite(DataGrid DG_MeasuresUnits, ComboBox CB_Measure, ComboBox CB_Unit)
        {
            // Validaciones
            if (CB_Measure.SelectedItem == null || CB_Unit.SelectedItem == null)
            {
                MessageBox.Show("Empty fields found.", "Error", MessageBoxButton.OK);
                return;
            }

            // Se recrean los objetos componentes
            SDA_Core.Business.Measure selectedMeasure = (SDA_Core.Business.Measure)CB_Measure.SelectedItem;
            SDA_Core.Business.Unit selectedUnit = (SDA_Core.Business.Unit)CB_Unit.SelectedItem;

            // Se crea el nuevo objeto, se valida y se añade
            SDA_Core.Business.MeasureUnit newMeasureUnit = new SDA_Core.Business.MeasureUnit(selectedMeasure, selectedUnit);
            if (measureUnitArray.List.Equals(newMeasureUnit))
            {
                MessageBox.Show("Value alredy exists.", "Error", MessageBoxButton.OK);
                return;
            }
            measureUnitArray.List.Add(newMeasureUnit);

            // Se reinicializa los valores y se actualiza la tabla.
            CB_Measure.SelectedIndex = -1;
            CB_Unit.SelectedIndex = -1;
            UpdateTable(DG_MeasuresUnits);
        }

        /// <summary>
        /// ES: Elimina un valor del dataGrid.
        /// </summary>
        /// <param name="DG_MeasuresUnits">>ES: DataGrid al que se deban hacer los cambios.</param>
        public void DeleteMeasureUnit(DataGrid DG_MeasuresUnits)
        {
            if (DG_MeasuresUnits.SelectedItem == null) return;
            int selected = DG_MeasuresUnits.SelectedIndex;
            measureUnitArray.List.Remove(selected);
            UpdateTable(DG_MeasuresUnits);
        }

        /// <summary>
        /// ES: Carga al datagrid con valores que se pasan como parametro.
        /// </summary>
        /// <param name="DG_MeasuresUnits">>ES: DataGrid al que se deban hacer los cambios.</param>
        /// <param name="data">ES: Datos a cargar.</param>
        public void LoadDataGrid(DataGrid DG_MeasuresUnits, SDA_Core.Business.Arrays.MeasureUnitArray data)
        {
            measureUnitArray = data;
            UpdateTable(DG_MeasuresUnits);
        }

        /// <summary>
        /// ES: Devuelve los datos que se muestran en el dataGrid.
        /// </summary>
        public SDA_Core.Business.Arrays.MeasureUnitArray GetData() => measureUnitArray;
    }
}