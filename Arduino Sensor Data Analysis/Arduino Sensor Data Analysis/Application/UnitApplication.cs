using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    /// <summary>
    /// ES: Clase para manejar los flujos de datos de la ventana 'Unit'
    /// </summary>
    public class UnitApplication
    {
        public UnitApplication()
        {
        }

        public SDA_Core.Business.Arrays.UnitArray GetData() => Data.UnitsDataArray;

        /// <summary>
        /// ES: Guarda los nuevos datos ingresados al binario.
        /// </summary>
        /// <param name="data">ES: Nuevos datos a guardar</param>
        public void SaveDataToBinary(SDA_Core.Business.Arrays.UnitArray data)
        {
            SDA_Core.Data.UnitSerializer.Save(data);
        }
    }
}