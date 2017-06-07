using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    public class UnitApplication : SDA_Core.Business.Arrays.UnitArray
    {
            public UnitApplication()
            {
                List<SDA_Core.Business.Unit> data = SDA_Core.Data.UnitSerializer.Serializer.RecoverData();
                foreach (SDA_Core.Business.Unit value in data)
                {
                    this.List.Add(value);
                }
            }

            public void SaveData(SDA_Core.Business.Arrays.UnitArray newData) => List = newData.List;

            public SDA_Core.Business.Arrays.UnitArray GetData() => this;

            public void SaveDataToBinary(SDA_Core.Business.Arrays.UnitArray data)
            {
                SDA_Core.Data.UnitSerializer.Serializer.ClearBinary();
                for (int i = 0; i < data.List.Size; ++i)
                {
                    SDA_Core.Data.UnitSerializer.Serializer.SaveData(data.List[i]);
                }
            }
    }
}
