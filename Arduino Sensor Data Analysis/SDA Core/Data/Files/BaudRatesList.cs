using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDA_Core.Communication;

namespace SDA_Core.Data.Files
{
    public class BaudRatesList
    {
        List<Communication.BaudRates> _baudRatesList;

        public List<BaudRates> BaudRates
        {
            get { return _baudRatesList; }
            set { _baudRatesList = value; }
        }

        public BaudRatesList()
        {
            _baudRatesList = new List<Communication.BaudRates>();
        }
    }
}
