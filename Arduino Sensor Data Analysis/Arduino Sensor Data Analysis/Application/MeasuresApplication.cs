using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    class MeasuresApplication 
    {
        public MeasuresApplication() { }

        public SDA_Core.Business.Arrays.MeasureArray GetData() => Data.MeasuresDataArray;

        public void SaveDataToBinary(SDA_Core.Business.Arrays.MeasureArray data)
        {
            SDA_Core.Data.MeasuresSerializer.Save(data);
        }
    
    }
}
