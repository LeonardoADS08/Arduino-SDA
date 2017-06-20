using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SDA_Program.Application
{
    public class UnitApplication 
    {
        public UnitApplication() { }

        public SDA_Core.Business.Arrays.UnitArray GetData() => Data.UnitsDataArray;

        public void SaveDataToBinary(SDA_Core.Business.Arrays.UnitArray data)
        {
            SDA_Core.Data.UnitSerializer.Save(data);
        }
    }
}
