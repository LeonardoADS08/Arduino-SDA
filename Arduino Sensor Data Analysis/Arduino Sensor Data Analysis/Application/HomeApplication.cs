using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    class HomeApplication
    {

        public HomeApplication() { }

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
