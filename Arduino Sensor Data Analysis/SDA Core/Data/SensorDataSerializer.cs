using SDA_Core.Business.Arrays;
using SDA_Core.Functional;
using System.Collections.Generic;
using System.Windows;

namespace SDA_Core.Data
{
    public static class SensorDataSerializer
    {
        static private Functional.DataSerializer<SensorDataFile> _serializer;

        static SensorDataSerializer()
        {
            _serializer = new DataSerializer<SensorDataFile>();
        }

        static private SensorData ConvertToBusiness(SensorDataFile value)
        {
            SensorData result = new SensorData();
            result.IdSensor = value.Id;
            result.SensorName = value.SensorName;
            foreach (int idMeasureUnit in value.IdMeasuresUnits)
            {
                result.AddColumn(MeasureUnitSerializer.Get(idMeasureUnit));
            }

            return result;
        }

        static private SensorDataFile ConvertToFile(SensorData value)
        {
            return new SensorDataFile(value);
        }

        static public bool Exists(SensorData value)
        {
            List<SensorDataFile> data = _serializer.RecoverData();
            SensorDataFile temp = new SensorDataFile(value);
            foreach (SensorDataFile val in data)
            {
                if (val.IdMeasuresUnits == temp.IdMeasuresUnits) return true;
            }
            return false;
        }

        static public SensorData Get(int Id)
        {
            List<SensorDataFile> data = _serializer.RecoverData();
            foreach (SensorDataFile val in data)
            {
                if (!val.Deleted && val.Id == Id) return ConvertToBusiness(val);
            }
            return null;
        }

        static public List<SensorData> Get()
        {
            List<SensorData> result = new List<SensorData>();
            List<SensorDataFile> data = _serializer.RecoverData();
            foreach (SensorDataFile value in data)
            {
                if (!value.Deleted) result.Add(ConvertToBusiness(value));
            }
            return result;
        }

        static public void Delete(int Id)
        {
            List<SensorDataFile> data = _serializer.RecoverData();
            for (int i = 0; i < data.Count; ++i)
            {
                if (data[i].Id == Id) data[i].Deleted = true;
            }
            _serializer.ClearBinary();
            _serializer.SaveData(data);
        }

        static public void Save(Business.Arrays.SensorDataArray values)
        {
            _serializer.ClearBinary();
            for (int i = 0; i < values.List.Size; ++i)
            {
                if (Exists(values.List[i]))
                {
                    MessageBox.Show("Value: " + values.List[i].SensorName + " alredy exists.");
                    continue;
                }

                if (values.List[i].IdSensor == -1)
                {
                    values.List[i].IdSensor = FilesConfigurations.SensorDataId();
                    FilesConfigurations.PlusSensorDataId();
                }

                _serializer.SaveData(ConvertToFile(values.List[i]));
            }
        }
    }
}