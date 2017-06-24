using System;

namespace SDA_Core.Business
{
    [Serializable]
    public class Measure
    {
        private int _IdMeasure;
        private string _name;

        public string Name { get => _name; set => _name = value; }
        public int IdMeasure { get => _IdMeasure; set => _IdMeasure = value; }

        public Measure()
        {
            _IdMeasure = -1;
            _name = "";
        }

        public Measure(string name)
        {
            _IdMeasure = -1;
            _name = name;
        }
    }
}