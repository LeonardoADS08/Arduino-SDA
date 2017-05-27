using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data
{
    class GenericArray<T>
    {
        private int _size;
        private T[] _data = new T[50];
        
        /// <summary>
        /// ES: Tamaño del arreglo.
        /// </summary>
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// ES: Datos del arreglo.
        /// </summary>
        public T[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// ES: Constructor de la clase génerica Arreglo.
        /// </summary>
        public GenericArray()
        {
            _size = 0;
        }

        /// <summary>
        /// ES: Añade un nuevo elemento al arreglo.
        /// </summary>
        /// <param name="element">ES: Nuevo elemento a añadir.</param>
        public void Add(T element)
        {
            _data[_size] = element;
            _size++;
        }

        /// <summary>
        /// ES: Elimina un dato del arreglo.
        /// </summary>
        /// <param name="position">ES: Posición del dato que se desea eliminar.</param>
        public void Delete(int position)
        {
            for (int i = position; i < _size - 1; ++i)
            {
                _data[i] = _data[i + 1];
            }
            _size--;
        }

    }
}
