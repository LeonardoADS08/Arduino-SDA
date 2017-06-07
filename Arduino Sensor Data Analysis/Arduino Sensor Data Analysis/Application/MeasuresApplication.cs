using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    class MeasuresApplication : SDA_Core.Business.Arrays.MeasureArray
    {
        public MeasuresApplication()
        {
            List<SDA_Core.Business.Measure> data = SDA_Core.Data.MeasuresSerializer.Serializer.RecoverData();
            foreach (SDA_Core.Business.Measure value in data)
            {
                this.List.Add(value);
            }
        }

        public void SaveData(SDA_Core.Business.Arrays.MeasureArray newData) => List = newData.List;

        public SDA_Core.Business.Arrays.MeasureArray GetData() => this;

        public void SaveDataToBinary(SDA_Core.Business.Arrays.MeasureArray data)
        {
            SDA_Core.Data.MeasuresSerializer.Serializer.ClearBinary();
            for (int i = 0; i < data.List.Size; ++i)
            {
                SDA_Core.Data.MeasuresSerializer.Serializer.SaveData(data.List[i]);
            }
        }
    
    }
}
