using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    public class BaudRatesApplication
    {

        public BaudRatesApplication() { }

        public SDA_Core.Business.Arrays.BaudRatesArray GetData() => Data.BaudRatesDataArray;

        public void SaveDataToBinary(SDA_Core.Business.Arrays.BaudRatesArray data)
        {
            SDA_Core.Data.BaudRatesSerializer.Save(data);
        }
        
    }
}
