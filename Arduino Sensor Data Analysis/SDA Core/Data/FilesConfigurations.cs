using SDA_Core.Functional;
using System.Collections.Generic;

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