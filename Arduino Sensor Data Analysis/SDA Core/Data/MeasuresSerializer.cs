using SDA_Core.Business;
using SDA_Core.Functional;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace SDA_Core.Data
{
    public class MeasuresSerializer
    {
        static private Functional.DataSerializer<MeasuresFile> _serializer;

        static MeasuresSerializer()
        {
            _serializer = new DataSerializer<MeasuresFile>();
        }

        static private Measure ConvertToBusiness(MeasuresFile value)
        {
            Measure result = new Measure();
            result.IdMeasure = value.Id;
            result.Name = value.Measure;

            return result;
        }

        static private MeasuresFile ConvertToFile(Measure value)
        {
            return new MeasuresFile(value);
        }

        static public bool Exists(Measure value)
        {
            List<MeasuresFile> data = _serializer.RecoverData();
            foreach (MeasuresFile val in data)
            {
                if (!val.Deleted && val.Measure == value.Name) return true;
            }
            return false;
        }

        static public Measure Get(int Id)
        {
            List<MeasuresFile> data = _serializer.RecoverData();
            foreach (MeasuresFile value in data)
            {
                if (!value.Deleted && value.Id == Id) return ConvertToBusiness(value);
            }
            return null;
        }

        static public List<Measure> Get()
        {
            List<Measure> result = new List<Measure>();
            List<MeasuresFile> data = _serializer.RecoverData();
            foreach (MeasuresFile value in data)
            {
                if (!value.Deleted) result.Add(ConvertToBusiness(value));
            }
            return result;
        }

        static public void Delete(int Id)
        {
            List<MeasuresFile> data = _serializer.RecoverData();
            for (int i = 0; i < data.Count; ++i)
            {
                if (data[i].Id == Id) data[i].Deleted = true;
            }
            _serializer.ClearBinary();
            _serializer.SaveData(data);
        }

        static public void Save(Business.Arrays.MeasureArray values)
        {
            _serializer.ClearBinary();
            for (int i = 0; i < values.List.Size; ++i)
            {
                if (Exists(values.List[i]))
                {
                    MessageBox.Show("Value: " + values.List[i].Name + " alredy exists.");
                    continue;
                }

                if (values.List[i].IdMeasure == -1)
                {
                    values.List[i].IdMeasure = FilesConfigurations.MeasuresId();
                    FilesConfigurations.PlusMeasuresId();
                }

                Debug.WriteLine("IdActual: " + values.List[i].IdMeasure);
                _serializer.SaveData(ConvertToFile(values.List[i]));
            }
        }
    }
}