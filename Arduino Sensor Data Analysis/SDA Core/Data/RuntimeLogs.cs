using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace SDA_Core.Data
{
    /// <summary>
    /// ES: Una clase que almacena registros en tiempo de ejecución, enfocado para guardar mensajes de excepciones u otros errores, 
    ///     para que sea posteriormente analizado y sea más fácil encontrar errores.
    ///     Este proceso se ejecuta en otro hilo.
    /// </summary>
    public static class RuntimeLogs
    {

        private static void WriteLine(string message, string direction, bool enumerate = true)
        {
            try
            {
                // ES: Dirección donde se guardará el archivo.
                //     Dirección del programa + "Runtime_Log " + Fecha + ".log"
                string logfile = AppDomain.CurrentDomain.BaseDirectory + "Runtime_Log " + DateTime.Today.ToString("dd-MM-yyyy") + ".log";
                
                // ES: El mensaje que será guardado en el Log
                string final = "";
                if (enumerate) final = DateTime.Now.ToString("hh:mm:ss") + ": " + direction + "\n" + message + "\r\n";
                else final = direction + "\n" + message + "\r\n\n\n";
                File.AppendAllText(logfile, final);
            }
            catch (Exception ex) { MessageBox.Show("A error message cannot be saved in the runtime log.\n" + ex.Message, "Error - RuntimeLog", MessageBoxButtons.OK); }
        }

        /// <summary>
        /// ES: Envia un log a los registros del programa, esta función intentará ejecutarse en otro hilo de proceso.
        /// </summary>
        public static void SendLog(string message, string direction, bool enumerate = true)
        {
            try
            {
                Thread _thread = new Thread(() => WriteLine(message, direction, enumerate));
                _thread.Start();
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message, nameof(RuntimeLogs) + "SendLog(string, string, bool = true)", false);
                WriteLine(message, direction, enumerate);
            }
            
        }

    }
}