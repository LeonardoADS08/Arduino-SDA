using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    /// <summary>
    /// ES: Clase para manejar los flujos de datos de la ventana 'Measures'
    /// </summary>
    class MeasuresApplication 
    {
        public MeasuresApplication() { }

        public SDA_Core.Business.Arrays.MeasureArray GetData() => Data.MeasuresDataArray;

        /// <summary>
        /// ES: Guarda los nuevos datos ingresados al binario.
        /// </summary>
        /// <param name="data">ES: Nuevos datos a guardar</param>
        public void SaveDataToBinary(SDA_Core.Business.Arrays.MeasureArray data)
        {
            SDA_Core.Data.MeasuresSerializer.Save(data);
        }
    
    }
}
