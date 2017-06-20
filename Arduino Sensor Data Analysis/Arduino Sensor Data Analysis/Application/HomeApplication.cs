using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    /// <summary>
    /// ES: Clase para manejar los flujos de datos de la ventana 'Home'
    /// </summary>
    class HomeApplication
    {

        public HomeApplication() { }

        /// <summary>
        /// ES: Devuelve los datos almacenados del arreglo 'BaudRates' del programa.
        /// </summary>
        /// <returns>ES: Datos que existen durante la ejecución del programa.</returns>
        public SDA_Core.Business.Arrays.BaudRatesArray GetBaudRates()
        {
            SDA_Core.Business.Arrays.BaudRatesArray result = new SDA_Core.Business.Arrays.BaudRatesArray();
            List<SDA_Core.Business.BaudRates> binary = Data.BaudRatesDataList;
            foreach (SDA_Core.Business.BaudRates value in binary)
            {
                result.List.Add(value);
            }
            return result;
        }

        /// <summary>
        /// ES: Devuelve los datos almacenados del arreglo 'Sensors' del programa.
        /// </summary>
        /// <returns>ES: Datos que existen durante la ejecución del programa.</returns>
        public SDA_Core.Business.Arrays.SensorDataArray GetSensors()
        {
            SDA_Core.Business.Arrays.SensorDataArray result = new SDA_Core.Business.Arrays.SensorDataArray();
            List<SDA_Core.Business.Arrays.SensorData> binary = Data.SensorsDataList;
            foreach (var value in binary)
            {
                result.List.Add(value);
            }
            return result;
        }


    }
}
