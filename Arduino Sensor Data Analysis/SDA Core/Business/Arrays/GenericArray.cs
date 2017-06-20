using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Business.Arrays
{
    [Serializable]
    public class GenericArray<T>
    {
        private List<T> _array;

        public int Size { get => _array.Count; }

        public T this[int key]
        {
            get => _array[key];
            set => _array[key] = value;
        }

        public GenericArray() => _array = new List<T>();

        public GenericArray(List<T> data) => _array = data;

        public void Add(T data) => _array.Add(data); 

        public void Clear() => _array.Clear(); 

        public void Remove(int position) => _array.RemoveAt(position);

        public bool Exists(T data) => _array.Any(value => value.Equals(data));

        public T First() => _array.First();

        public T Last() => _array.Last();

        public List<T> GetList() => _array;
    }
}
