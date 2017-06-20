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
    public class BaudRatesInterface
    {
        // Almacenamiento para los dataGrids.
        SDA_Core.Business.Arrays.BaudRatesArray baudRates;

        // Objeto de una clase 'funcional' para hacer transformaciones.
        SDA_Core.Functional.Data dataManager;

        /// <summary>
        /// ES: Constructor de la clase BaudRatesInterface.
        /// </summary>
        public BaudRatesInterface()
        {
            baudRates = new SDA_Core.Business.Arrays.BaudRatesArray();
            dataManager = new SDA_Core.Functional.Data();
        }

        /// <summary>
        /// ES: Método para actualizar la tabla con los nuevos datos que se hayan guardado/eliminado.
        /// </summary>
        /// <param name="DG_BaudRates">ES: DataGrid al que se deban hacer los cambios.</param>
        public void UpdateTable(DataGrid DG_BaudRates) => DG_BaudRates.ItemsSource = dataManager.BaudRatesListToDataTable(baudRates).AsDataView();

        /// <summary>
        /// ES: Método para generar un nuevo BaudRate a partir de los datos ingresados por el usuario.
        /// </summary>
        /// <param name="DG_BaudRates">ES: DataGrid al que se deban hacer los cambios.</param>
        /// <param name="TB_BaudRate">ES: TextBox que contiene el valor del nuevo BaudRate</param>
        public void NewBaudRate(DataGrid DG_BaudRates, TextBox TB_BaudRate)
        {
            // Validaciones
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
            
            // Se genera el objeto y se valida que no exista ya.
            SDA_Core.Business.BaudRates newBaudRate = new SDA_Core.Business.BaudRates(baudRateValue);
            if (baudRates.List.Exists(newBaudRate))
            {
                MessageBox.Show("Value alredy exists.", "Error", MessageBoxButton.OK);
                return;
            }

            // Se añade al dataGrid, se limpian los campos y se actualiza la tabla.
            baudRates.List.Add(newBaudRate);
            TB_BaudRate.Text = "";
            UpdateTable(DG_BaudRates);
        }

        /// <summary>
        /// ES: Elimina un valor del dataGrid.
        /// </summary>
        /// <param name="DG_BaudRates">>ES: DataGrid al que se deban hacer los cambios.</param>
        public void DeleteBaudRate(DataGrid DG_BaudRates)
        {
            if (DG_BaudRates.SelectedItem == null) return;
            int selected = DG_BaudRates.SelectedIndex;
            baudRates.List.Remove(selected);
            UpdateTable(DG_BaudRates);
        }

        /// <summary>
        /// ES: Carga al datagrid con valores que se pasan como parametro.
        /// </summary>
        /// <param name="DG_BaudRates">>ES: DataGrid al que se deban hacer los cambios.</param>
        /// <param name="data">ES: Datos a cargar.</param>
        public void LoadDataGrid(DataGrid DG_BaudRates, SDA_Core.Business.Arrays.BaudRatesArray data)
        {
            baudRates = data;
            UpdateTable(DG_BaudRates);
        }

        /// <summary>
        /// ES: Devuelve los datos que se muestran en el dataGrid.
        /// </summary>
        public SDA_Core.Business.Arrays.BaudRatesArray GetData() => baudRates;

    }
}
