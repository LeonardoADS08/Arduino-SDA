﻿using System;
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
            List<SDA_Core.Business.Measure> binary = Data.MeasuresDataList;
            foreach (var value in binary)
            {
                result.List.Add(value);
            }
            return result;
        }

        public SDA_Core.Business.Arrays.UnitArray GetUnits()
        {
            SDA_Core.Business.Arrays.UnitArray result = new SDA_Core.Business.Arrays.UnitArray();
            List<SDA_Core.Business.Unit> binary = Data.UnitsDataList;
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

        public SDA_Core.Business.Arrays.BaudRatesArray GetBaudRates()
        {
            SDA_Core.Business.Arrays.BaudRatesArray result = new SDA_Core.Business.Arrays.BaudRatesArray();
            List<SDA_Core.Business.BaudRates> binary = Data.BaudRatesDataList;
            foreach (var value in binary)
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
