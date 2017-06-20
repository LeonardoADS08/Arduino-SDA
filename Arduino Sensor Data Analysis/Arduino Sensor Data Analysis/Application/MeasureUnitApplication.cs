using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    class MeasureUnitApplication
    {
        public MeasureUnitApplication() { }

        public SDA_Core.Business.Arrays.MeasureUnitArray GetData() => Data.MeasureUnitsDataArray;

        public void SaveDataToBinary(SDA_Core.Business.Arrays.MeasureUnitArray data)
        {
            SDA_Core.Data.MeasureUnitSerializer.Save(data);
        }

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
    }
}
