namespace SDA_Core.Business.Arrays
{
    public class PortArray : Port
    {
        private GenericArray<Port> _list;

        public GenericArray<Port> List { get => _list; set => _list = value; }

        public PortArray()
        {
            _list = new GenericArray<Port>();
        }
    }
}