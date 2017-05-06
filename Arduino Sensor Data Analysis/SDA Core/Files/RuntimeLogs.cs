using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace SDA_Core
{
    /// <summary>
    /// ES: Una clase que almacena registros en tiempo de ejecución, enfocado para guardar mensajes de excepciones u otros errores, 
    ///     para que sea posteriormente analizado y sea más fácil encontrar errores.
    /// </summary>
    public static class RuntimeLogs
    {
        public static void WriteLine(string message, bool enumerate = true)
        {
            try
            {
                // ES: Dirección donde se guardará el archivo.
                //     Dirección del programa + "Runtime_Log " + Fecha + ".log"
                string logfile = AppDomain.CurrentDomain.BaseDirectory + "Runtime_Log " + DateTime.Today.ToString("dd-MM-yyyy") + ".log";
                
                // ES: El mensaje que será guardado en el Log
                string final = "";
                if (enumerate) final = DateTime.Now.ToString("hh:mm:ss") + ": " + message + "\r\n";
                else final = message + "\r\n\n\n";
                File.AppendAllText(logfile, final);
            }
            catch (Exception ex) { MessageBox.Show("A error message cannot be saved in the runtime log.\n" + ex.Message, "Error - RuntimeLog", MessageBoxButtons.OK); }
        }
    }
}