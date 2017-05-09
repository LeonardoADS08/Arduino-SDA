using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Communication
{
    /// <summary>
    /// ES: Clase abstracta que contiene los métodos básicos para considerar una conexión.
    /// </summary>
    abstract public class Connection
    {
        abstract public void Write(string message);
        abstract public string Read();
        abstract public bool Available();
        abstract public void Open();
        abstract public void Close();
    }
}
