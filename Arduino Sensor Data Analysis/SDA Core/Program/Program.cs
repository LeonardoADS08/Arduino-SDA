using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDA_Core.Analysis;
using SDA_Core.Communication;

namespace SDA_Core
{
    public class Program
    {
        static private Analysis.Statistics _statistics;
        static public SDA_Core.Data.Containers.SensorData _data = new Data.Containers.SensorData(2);
        public static Statistics Statistics
        {
            get { return _statistics; }
            set { _statistics = value; }
        }

        static Program()
        {
            _statistics = new Statistics();
        }

        public Program()
        {
            
        }
    }
}
