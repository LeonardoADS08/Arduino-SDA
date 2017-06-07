using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    class MeasureUnitApplication : SDA_Core.Business.Arrays.MeasureUnitArray
    {
        public MeasureUnitApplication()
        {
            List<SDA_Core.Business.MeasureUnit> data = SDA_Core.Data.MeasureUnitSerializer.Serializer.RecoverData();
            foreach (SDA_Core.Business.MeasureUnit value in data)
            {
                this.List.Add(value);
            }
        }

        public void SaveData(SDA_Core.Business.Arrays.MeasureUnitArray newData) => List = newData.List;

        public SDA_Core.Business.Arrays.MeasureUnitArray GetData() => this;

        public void SaveDataToBinary(SDA_Core.Business.Arrays.MeasureUnitArray data)
        {
            SDA_Core.Data.MeasureUnitSerializer.Serializer.ClearBinary();
            for (int i = 0; i < data.List.Size; ++i)
            {
                SDA_Core.Data.MeasureUnitSerializer.Serializer.SaveData(data.List[i]);
            }
        }

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
    }
}
