using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    public class BaudRatesApplication : SDA_Core.Business.Arrays.BaudRatesArray
    {

        public BaudRatesApplication()
        {
            List<SDA_Core.Business.BaudRates> data = SDA_Core.Data.BaudRatesSerializer.Serializer.RecoverData();
            foreach (SDA_Core.Business.BaudRates value in data)
            {
                this.List.Add(value);
            }
        }

       /* public void SaveData(SDA_Core.Business.Arrays.BaudRatesArray newData)
        {
            List = newData.List;
            SaveDataToBinary(newData);
        }*/

        public SDA_Core.Business.Arrays.BaudRatesArray GetData() => this;

        public void SaveDataToBinary(SDA_Core.Business.Arrays.BaudRatesArray data)
        {
                SDA_Core.Data.BaudRatesSerializer.Serializer.ClearBinary();
                for (int i = 0; i < data.List.Size; ++i)
                {
                    SDA_Core.Data.BaudRatesSerializer.Serializer.SaveData(data.List[i]);
                }
        }
        
    }
}
