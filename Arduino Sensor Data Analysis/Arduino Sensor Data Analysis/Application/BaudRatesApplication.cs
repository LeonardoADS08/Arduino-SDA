using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program.Application
{
    /// <summary>
    /// ES: Clase para manejar los flujos de datos de la ventana 'BaudRates'
    /// </summary>
    public class BaudRatesApplication
    {

        public BaudRatesApplication() { }

        /// <summary>
        /// ES: Devuelve los datos almacenados en los arreglos del programa.
        /// </summary>
        /// <returns></returns>
        public SDA_Core.Business.Arrays.BaudRatesArray GetData() => Data.BaudRatesDataArray;

        /// <summary>
        /// ES: Guarda los nuevos datos ingresados al binario.
        /// </summary>
        /// <param name="data">ES: Nuevos datos a guardar</param>
        public void SaveDataToBinary(SDA_Core.Business.Arrays.BaudRatesArray data)
        {
            SDA_Core.Data.BaudRatesSerializer.Save(data);
        }
        
    }
}
