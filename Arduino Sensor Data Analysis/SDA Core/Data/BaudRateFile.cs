using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Core.Data
{
    [Serializable]
    public class BaudRateFile
    {
        private int _id;
        private int _baudRate;
        private bool _deleted;

        public int Id { get => _id; set => _id = value; }
        public int BaudRate { get => _baudRate; set => _baudRate = value; }
        public bool Deleted { get => _deleted; set => _deleted = value; }

        public BaudRateFile() { _id = -1; }

        public BaudRateFile(Business.BaudRates value)
        {
            _id = value.IdBaudRate;
            _baudRate = value.Value;
            _deleted = false;
        }

    }
}
