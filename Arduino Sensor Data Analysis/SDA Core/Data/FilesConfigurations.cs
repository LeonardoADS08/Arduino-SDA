using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Data;
using System.Diagnostics;
using SDA_Core;
using SDA_Core.Business;
using SDA_Core.Functional;

namespace SDA_Core.Data
{
    static public class FilesConfigurations
    {
        static private DataSerializer<int> _serializer;

        static FilesConfigurations()
        {
            _serializer = new DataSerializer<int>("Persistance.data");
            if (_serializer.RecoverData().Count == 0)
            {
                for (int i = 0; i < 5; ++i) _serializer.SaveData(0);
            }
        }

        static public int BaudRatesId() => _serializer.RecoverData()[0];
        static public int MeasuresId() => _serializer.RecoverData()[1];
        static public int UnitsId() => _serializer.RecoverData()[2];
        static public int MeasuresUnitId() => _serializer.RecoverData()[3];
        static public int SensorDataId() => _serializer.RecoverData()[4];

        static public void PlusBaudRatesId()
        {
            List<int> values = _serializer.RecoverData();
            values[0]++;
            _serializer.ClearBinary();
            _serializer.SaveData(values);
        }

        static public void PlusMeasuresId()
        {
            List<int> values = _serializer.RecoverData();
            values[1]++;
            _serializer.ClearBinary();
            _serializer.SaveData(values);

        }

        static public void PlusUnitsId()
        {
            List<int> values = _serializer.RecoverData();
            values[2]++;
            _serializer.ClearBinary();
            _serializer.SaveData(values);
        }

        static public void PlusMeasuresUnitId()
        {
            List<int> values = _serializer.RecoverData();
            values[3]++;
            _serializer.ClearBinary();
            _serializer.SaveData(values);
        }

        static public void PlusSensorDataId()
        {
            List<int> values = _serializer.RecoverData();
            values[4]++;
            _serializer.ClearBinary();
            _serializer.SaveData(values);
        }
    }
}
