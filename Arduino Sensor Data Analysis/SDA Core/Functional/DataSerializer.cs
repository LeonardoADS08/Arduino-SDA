using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;

namespace SDA_Core.Functional
{
    /// <summary>
    /// ES: Clase generica encargada de almacenar y leer archivos binarios de tipo <T>. 
    ///     NOTA: La clase pasada para instanciar este objeto debe ser Serializable.
    /// </summary>
    public class DataSerializer<T>
    {
        // ES: _fileName  = Nombre del archivo donde se guardara o se deberá leer los datos serializados  
        private string _fileDirection;
        // ES: _formatter = Objeto para darle formato binario a los archivos a escribir                     
        private IFormatter _formatter;

        /// <summary>
        /// ES: Dirección del archivo.
        /// </summary>
        public string FileDirection { get => _fileDirection; }

        /// <summary>
        /// ES: Instancia de una clase Stream para escritura.
        /// </summary>
        /// <returns>ES: Stream en modo escritura.</returns>
        private Stream WriteStream() => new FileStream(_fileDirection, FileMode.Append, FileAccess.Write, FileShare.ReadWrite); 


        /// <summary>
        /// ES: Instancia de una clase Stream para lectura.
        /// </summary>
        /// <returns>ES: Stream en modo lectura.</returns>
        private Stream ReadStream() => new FileStream(_fileDirection, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);

        /// <summary>
        /// ES: Constructor de la clase DataSerializer, dictará cual será la dirección del archivo binario.
        /// </summary>
        public DataSerializer()
        {
            try
            {
                _fileDirection = AppDomain.CurrentDomain.BaseDirectory + typeof(T).Name + ".data";
                _formatter = new BinaryFormatter();
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).Name + ".DataSerializer()"); }
        }

        /// <summary>
        /// ES: Constructor de la clase DataSerializer.
        /// </summary>
        /// <param name="FileName">ES: Nombre del archivo binario.</param>
        public DataSerializer(string FileName)
        {
            try
            {
                _fileDirection = AppDomain.CurrentDomain.BaseDirectory + FileName;
                _formatter = new BinaryFormatter();
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).Name + ".DataSerializer(string)"); }
        }

        /// <summary>
        /// ES: Guarda elementos en el archivo binario.
        /// </summary>
        /// <param name="data">ES: Dato a almacenar en el archivo binario.</param>
        public void SaveData(T data)
        {
            try
            {
                _formatter.Serialize(WriteStream(), data);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).Name + ".SaveData(T)"); }
        }

        /// <summary>
        /// ES: Guarda todos los elementos almacenados en una lista en el archivo binario.
        /// </summary>
        /// <param name="data">ES: Lista que contiene los datos.</param>
        public void SaveData(List<T> data)
        {
            try
            {
                for (int i = 0; i < data.Count; ++i) { SaveData(data[i]); }
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).Name + ".SaveData(List<T>)"); }
        }

        /// <summary>
        /// ES: Recupera todos los datos almacenados en el archivo binario en ese momento en una lista.
        /// </summary>
        /// <returns>ES: Devuelve todos los datos presentes en el binario en una lista.</returns>
        public List<T> RecoverData()
        {
            List<T> list = new List<T>();
            try
            {
                Stream stream = ReadStream();
                while (stream.Position < stream.Length)
                {
                    T result = (T)_formatter.Deserialize(stream);
                    list.Add(result);
                }
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).Name + ".RecoverAllData()"); }
            return list;
        }

        /// <summary>
        /// ES: Recupera un registro que se encuentre en el archivo binario.
        /// </summary>
        /// <param name="IdRegister">ES: Registro a devolver</param>
        /// <returns>ES: Devuelve un registro si es que existe en el binario, caso de que no exista devuelve un valor por defecto.</returns>
        public T RecoverData(int IdRegister)
        {
            try
            {
                /*Stream stream = ReadStream();
                int typeSize = Marshal.SizeOf(typeof(T));
                stream.Seek((IdRegister - 1) * typeSize, SeekOrigin.Begin);
                return (T)_formatter.Deserialize(stream);*/
                return this.RecoverData()[IdRegister];
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).Name + ".RecoverData(int)"); }
            return default(T);
        }

        /// <summary>
        /// ES: Elimina todos los datos del archivo binario.
        /// PRECAUCIÓN: Una vez eliminados, no es posible recuperarlos.
        /// </summary>
        public void ClearBinary()
        {
            try
            {
                Stream stream = new FileStream(_fileDirection, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).Name + ".ClearBinary()"); }
        }

        /// <summary>
        /// ES: Elimina un registro en el archivo binario.
        /// </summary>
        /// <param name="IdRegister">ES: Identificador del registro que se desea eliminar.</param>
        public void DeleteRegister(int IdRegister)
        {
            IdRegister--;
            try
            {
                List<T> list = this.RecoverData();
                list.RemoveAt(IdRegister);
                this.ClearBinary();
                this.SaveData(list);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).Name + ".DeleteRegister(int)"); }
        }

        /// <summary>
        /// ES: Actualiza un registro del archivo binario.
        /// </summary>
        /// <param name="IdRegister">ES: Identificador del registro que se desea actualizar.</param>
        /// <param name="data">ES: Elemento por el cual debe actualizado</param>
        public void UpdateData(int IdRegister, T data)
        {
            IdRegister--;
            try
            {
                List<T> list = this.RecoverData();
                list[IdRegister] = data;
                ClearBinary();
                SaveData(list);
            }
            catch (Exception ex) { RuntimeLogs.SendLog(ex.Message, typeof(DataSerializer<T>).Name + ".UpdateData(int, T)"); }
        }
    }
}
