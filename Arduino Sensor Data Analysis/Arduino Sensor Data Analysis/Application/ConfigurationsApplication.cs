using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    public class ConfigurationsApplication
    {

        public SDA_Core.Business.Arrays.MeasureArray GetMeasures()
        {
            SDA_Core.Business.Arrays.MeasureArray result = new SDA_Core.Business.Arrays.MeasureArray();
            List<SDA_Core.Business.Measure> binary = SDA_Core.Data.MeasuresSerializer.Serializer.RecoverData();
            foreach (var value in binary)
            {
                result.List.Add(value);
            }
            return result;
        }

        public SDA_Core.Business.Arrays.UnitArray GetUnits()
        {
            SDA_Core.Business.Arrays.UnitArray result = new SDA_Core.Business.Arrays.UnitArray();
            List<SDA_Core.Business.Unit> binary = SDA_Core.Data.UnitSerializer.Serializer.RecoverData();
            foreach (var value in binary)
            {
                result.List.Add(value);
            }
            return result;
        }

        public SDA_Core.Business.Arrays.MeasureUnitArray GetMeasureUnits()
        {
            SDA_Core.Business.Arrays.MeasureUnitArray result = new SDA_Core.Business.Arrays.MeasureUnitArray();
            List<SDA_Core.Business.MeasureUnit> binary = SDA_Core.Data.MeasureUnitSerializer.Serializer.RecoverData();
            foreach (var value in binary)
            {
                result.List.Add(value);
            }
            return result;
        }

        public SDA_Core.Business.Arrays.BaudRatesArray GetBaudRates()
        {
            SDA_Core.Business.Arrays.BaudRatesArray result = new SDA_Core.Business.Arrays.BaudRatesArray();
            List<SDA_Core.Business.BaudRates> binary = SDA_Core.Data.BaudRatesSerializer.Serializer.RecoverData();
            foreach (var value in binary)
            {
                result.List.Add(value);
            }
            return result;
        }

        public SDA_Core.Business.Arrays.SensorDataArray GetSensors()
        {
            SDA_Core.Business.Arrays.SensorDataArray result = new SDA_Core.Business.Arrays.SensorDataArray();
            List<SDA_Core.Business.Arrays.SensorData> binary = SDA_Core.Data.SensorDataSerializer.Serializer.RecoverData();
            foreach (var value in binary)
            {
                result.List.Add(value);
            }
            return result;
        }
    }
}
