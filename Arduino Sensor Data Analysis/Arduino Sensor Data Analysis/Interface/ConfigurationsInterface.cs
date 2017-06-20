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
    public class ConfigurationsInterface
    {
        // Objeto de una clase 'funcional' para hacer transformaciones.
        SDA_Core.Functional.Data dataManager;

        /// <summary>
        /// ES: Constructor de la clase ConfigurationsInterface
        /// </summary>
        public ConfigurationsInterface()
        {
            dataManager = new SDA_Core.Functional.Data();
        }

        /// <summary>
        /// ES: Carga a un dataGrid valroes de tipo 'Measure'.
        /// </summary>
        /// <param name="DG_Measures">ES: DataGrid al que se deban hacer los cambios.</param>
        /// <param name="data">ES: Datos a cargar.</param>
        public void LoadMeasures(DataGrid DG_Measures, SDA_Core.Business.Arrays.MeasureArray data)
        {
            DG_Measures.ItemsSource = dataManager.MeasureListToDataTable(data).AsDataView();
        }

        /// <summary>
        /// ES: Carga a un dataGrid valroes de tipo 'Unit'.
        /// </summary>
        /// <param name="DG_Units">ES: DataGrid al que se deban hacer los cambios.</param>
        /// <param name="data">ES: Datos a cargar.</param>
        public void LoadUnits(DataGrid DG_Units, SDA_Core.Business.Arrays.UnitArray data)
        {
            DG_Units.ItemsSource = dataManager.UnitListToDataTable(data).AsDataView();
        }

        /// <summary>
        /// ES: Carga a un dataGrid valroes de tipo 'MeasureUnit'.
        /// </summary>
        /// <param name="DG_MeasureUnits">ES: DataGrid al que se deban hacer los cambios.</param>
        /// <param name="data">ES: Datos a cargar.</param>
        public void LoadMeasureUnits(DataGrid DG_MeasureUnits, SDA_Core.Business.Arrays.MeasureUnitArray data)
        {
            DG_MeasureUnits.ItemsSource = dataManager.MeasureUnitListToDataTable(data).AsDataView();
        }

        /// <summary>
        /// ES: Carga a un dataGrid valroes de tipo 'BaudRates'.
        /// </summary>
        /// <param name="DG_BaudRates">ES: DataGrid al que se deban hacer los cambios.</param>
        /// <param name="data">ES: Datos a cargar.</param>
        public void LoadBaudRates(DataGrid DG_BaudRates, SDA_Core.Business.Arrays.BaudRatesArray data)
        {
            DG_BaudRates.ItemsSource = dataManager.BaudRatesListToDataTable(data).AsDataView();
        }

        /// <summary>
        /// ES: Carga a un dataGrid valroes de tipo 'Sensor'.
        /// </summary>
        /// <param name="DG_Sensors">ES: DataGrid al que se deban hacer los cambios.</param>
        /// <param name="data">ES: Datos a cargar.</param>
        public void LoadSensors(DataGrid DG_Sensors, SDA_Core.Business.Arrays.SensorDataArray data)
        {
            DG_Sensors.ItemsSource = dataManager.SensorsListToDataTable(data).AsDataView();
        }
    }
}
