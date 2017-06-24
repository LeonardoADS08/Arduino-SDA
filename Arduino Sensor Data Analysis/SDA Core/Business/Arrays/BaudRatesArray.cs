namespace SDA_Core.Business.Arrays
{
    public class BaudRatesArray : BaudRates
    {
        private GenericArray<BaudRates> _list;

        public GenericArray<BaudRates> List { get => _list; set => _list = value; }

        public BaudRatesArray()
        {
            _list = new GenericArray<BaudRates>();
        }
    }
}