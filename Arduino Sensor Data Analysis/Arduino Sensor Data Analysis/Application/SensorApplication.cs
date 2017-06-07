using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    class SensorApplication : SDA_Core.Business.Arrays.SensorDataArray
    {

        public SensorApplication()
        {

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

        public void SaveDataToBinary(SDA_Core.Business.Arrays.SensorDataArray data)
        {
            SDA_Core.Data.SensorDataSerializer.Serializer.ClearBinary();
            for (int i = 0; i < data.List.Size; ++i)
            {
                SDA_Core.Data.SensorDataSerializer.Serializer.SaveData(data.List[i]);
            }
        }
    }
}
