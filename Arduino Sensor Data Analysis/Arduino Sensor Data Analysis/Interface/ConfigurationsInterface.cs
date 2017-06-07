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
    class ConfigurationsInterface
    {
        SDA_Core.Functional.Data dataManager;

        public ConfigurationsInterface()
        {
            dataManager = new SDA_Core.Functional.Data();
        }

        public void LoadMeasures(DataGrid DG_Measures, SDA_Core.Business.Arrays.MeasureArray data)
        {
            DG_Measures.ItemsSource = dataManager.MeasureListToDataTable(data).AsDataView();
        }

        public void LoadUnits(DataGrid DG_Units, SDA_Core.Business.Arrays.UnitArray data)
        {
            DG_Units.ItemsSource = dataManager.UnitListToDataTable(data).AsDataView();
        }

        public void LoadMeasureUnits(DataGrid DG_MeasureUnits, SDA_Core.Business.Arrays.MeasureUnitArray data)
        {
            DG_MeasureUnits.ItemsSource = dataManager.MeasureUnitListToDataTable(data).AsDataView();
        }

        public void LoadBaudRates(DataGrid DG_BaudRates, SDA_Core.Business.Arrays.BaudRatesArray data)
        {
            DG_BaudRates.ItemsSource = dataManager.BaudRatesListToDataTable(data).AsDataView();
        }

        public void LoadSensors(DataGrid DG_Sensors, SDA_Core.Business.Arrays.SensorDataArray data)
        {
            DG_Sensors.ItemsSource = dataManager.SensorsListToDataTable(data).AsDataView();
        }
    }
}
