using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;

namespace SDA_Core.Files
{
    /// <summary>
    /// ES: Clase generica encargada de almacenar y leer archivos binarios de tipo <T>. 
    ///     NOTA: La clase pasada para instanciar este objeto debe ser Serializable.
    /// </summary>
    class DataSerializer<T>
    {
        // ES: _fileName  = Nombre del archivo donde se guardara o se deberá leer los datos serializados  
        private string _fileDirection;
        // ES: _stream    = Objeto stream para leer o escribir sobre un archivo                             
        private Stream _stream;
        // ES: _formatter = Objeto para darle formato binario a los archivos a escribir                     
        private IFormatter _formatter;

        public string FileDirection
        {
            get { return _fileDirection; }
        }

        /// <summary>
        /// ES: Constructor de la clase DataSerializer, la cual la dirección del archivo será la dirección de instalación mas el nombre de la clase.
        /// </summary>
        DataSerializer()
        {
            try
            {
                _fileDirection = AppDomain.CurrentDomain.BaseDirectory + nameof(T);
                _stream = new FileStream(_fileDirection + ".data", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                _formatter = new BinaryFormatter();
            }
            catch (Exception ex) { RuntimeLogs.WriteLine(ex.Message); }
        }

        /// <summary>
        /// ES: Constructor de la clase DataSerializer, permite escoger el nombre del archivo donde se guardará los datos, la dirección sigue siendo donde se encuentre instalado el programa.
        /// </summary>
        DataSerializer(string FileName)
        {
            try
            {
                _fileDirection = AppDomain.CurrentDomain.BaseDirectory + FileName;
                _stream = new FileStream(_fileDirection + ".data", FileMode.Append, FileAccess.ReadWrite, FileShare.ReadWrite);
                _formatter = new BinaryFormatter();
            }
            catch (Exception ex) { RuntimeLogs.WriteLine(ex.Message); }
        }

        /// <summary>
        /// ES: Guarda el dato pasado como parámetro en el archivo binario.
        /// </summary>
        public void SaveData(T data) { _formatter.Serialize(_stream, data); }

        /// <summary>
        /// ES: Recupera todos los datos almacenados en el archivo binario en ese momento.
        /// </summary>
        public List<T> RecoverAllData()
        {
            List<T> list = new List<T>();
            try
            {
                _stream.Position = 0;
                while (_stream.Position < _stream.Length)
                {
                    T result = (T)_formatter.Deserialize(_stream);
                    list.Add(result);
                }
            }
            catch (Exception ex) { RuntimeLogs.WriteLine(ex.Message); }
            return list;
        }

        /// <summary>
        /// ES: Recupera el registro que se encuentre en la posición 'IdRegister' en el archivo binario.
        /// </summary>
        public T RecoverData(int IdRegister)
        {
            try
            {
                int typeSize = Marshal.SizeOf(typeof(T));
                _stream.Seek((IdRegister - 1) * typeSize, SeekOrigin.Begin);
                return (T)_formatter.Deserialize(_stream);
            }
            catch (Exception ex) { RuntimeLogs.WriteLine(ex.Message); }
            return default(T);
        }
    }
}
