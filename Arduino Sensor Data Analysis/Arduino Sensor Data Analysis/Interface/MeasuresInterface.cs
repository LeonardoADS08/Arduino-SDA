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
    public class MeasuresInterface
    {
        // Arreglo para guardar los datos del dataGrid.
        SDA_Core.Business.Arrays.MeasureArray measuresArray;
        // Objeto de una clase 'funcional' para hacer transformaciones.
        SDA_Core.Functional.Data dataManager;

        /// <summary>
        /// ES: Constructor de la clase MeasuresInterface
        /// </summary>
        public MeasuresInterface()
        {
            measuresArray = new SDA_Core.Business.Arrays.MeasureArray();
            dataManager = new SDA_Core.Functional.Data();
        }

        /// <summary>
        ///  Método para actualizar la tabla con los nuevos datos que se hayan guardado/eliminado.
        /// </summary>
        /// <param name="DG_Measures">ES: DataGrid al que se deban hacer los cambios.</param>
        public void UpdateTable(DataGrid DG_Measures) => DG_Measures.ItemsSource = dataManager.MeasureListToDataTable(measuresArray).AsDataView();

        /// <summary>
        /// ES: Método para generar un nuevo 'Unit' a partir de los datos ingresados por el usuario.
        /// </summary>
        /// <param name="DG_Measures">ES: DataGrid al que se deban hacer los cambios.</param>
        /// <param name="TB_Measures">ES: Datos del nuevo objeto a crear</param>
        public void NewUnit(DataGrid DG_Measures, TextBox TB_Measures)
        {
            // Validaciones
            if (TB_Measures.Text == "")
            {
                MessageBox.Show("Empty fields.", "Error", MessageBoxButton.OK);
                return;
            }

            // Se crea el objeto, se valida y añade si se puede.
            SDA_Core.Business.Measure newMeasure = new SDA_Core.Business.Measure(TB_Measures.Text);
            if (measuresArray.List.Exists(newMeasure))
            {
                MessageBox.Show("Value alredy exists.", "Error", MessageBoxButton.OK);
                return;
            }
            measuresArray.List.Add(newMeasure);

            // Se limpia los datos y se actualiza el dataGrid.
            TB_Measures.Text = "";
            UpdateTable(DG_Measures);
        }

        /// <summary>
        /// ES: Elimina un valor del dataGrid.
        /// </summary>
        /// <param name="DG_Measures">ES: DataGrid al que se deban hacer los cambios.</param>
        public void DeleteBaudRate(DataGrid DG_Measures)
        {
            if (DG_Measures.SelectedItem == null) return;
            int selected = DG_Measures.SelectedIndex;
            measuresArray.List.Remove(selected);
            UpdateTable(DG_Measures);
        }

        /// <summary>
        /// ES: Carga al datagrid con valores que se pasan como parametro.
        /// </summary>
        /// <param name="DG_Measures">ES: DataGrid al que se deban hacer los cambios.</param>
        /// <param name="data">ES: Datos a cargar.</param>
        public void LoadDataGrid(DataGrid DG_Measures, SDA_Core.Business.Arrays.MeasureArray data)
        {
            measuresArray = data;
            UpdateTable(DG_Measures);
        }

        /// <summary>
        /// ES: Devuelve los datos que se muestran en el dataGrid.
        /// </summary>
        public SDA_Core.Business.Arrays.MeasureArray GetData() => measuresArray;
    }
}
