using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    /// <summary>
    /// ES: Clase para manejar los flujos de datos de la ventana 'Sensor'
    /// </summary>
    class SensorApplication : SDA_Core.Business.Arrays.SensorDataArray
    {

        public SensorApplication() { }

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

        public SDA_Core.Business.Arrays.MeasureUnitArray GetMeasureUnits()
        {
            SDA_Core.Business.Arrays.MeasureUnitArray result = new SDA_Core.Business.Arrays.MeasureUnitArray();
            List<SDA_Core.Business.MeasureUnit> binary = Data.MeasureUnitsDataList;
            foreach (var value in binary)
            {
                result.List.Add(value);
            }
            return result;
        }

        /// <summary>
        /// ES: Guarda los nuevos datos ingresados al binario.
        /// </summary>
        /// <param name="data">ES: Nuevos datos a guardar</param>
        public void SaveDataToBinary(SDA_Core.Business.Arrays.SensorDataArray data)
        {
            SDA_Core.Data.SensorDataSerializer.Save(data);
        }
    }
}
