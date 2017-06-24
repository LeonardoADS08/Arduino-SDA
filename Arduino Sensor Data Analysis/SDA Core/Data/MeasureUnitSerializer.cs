using SDA_Core.Business;
using SDA_Core.Functional;
using System.Collections.Generic;
using System.Windows;

namespace SDA_Core.Data
{
    static public class MeasureUnitSerializer
    {
        static private Functional.DataSerializer<MeasuresUnitFile> _serializer;

        static MeasureUnitSerializer()
        {
            _serializer = new DataSerializer<MeasuresUnitFile>();
        }

        static private MeasureUnit ConvertToBusiness(MeasuresUnitFile value)
        {
            MeasureUnit result = new MeasureUnit();
            result.IdMeasureUnit = value.Id;
            result.Measure = MeasuresSerializer.Get(value.IdMeasure);
            result.Unit = UnitSerializer.Get(value.IdUnit);

            return result;
        }

        static private MeasuresUnitFile ConvertToFile(MeasureUnit value)
        {
            return new MeasuresUnitFile(value);
        }

        static public bool Exists(MeasureUnit value)
        {
            List<MeasuresUnitFile> data = _serializer.RecoverData();
            foreach (MeasuresUnitFile br in data)
            {
                if (!br.Deleted &&
                    br.IdMeasure == value.Measure.IdMeasure &&
                    br.IdUnit == value.Unit.IdUnit) return true;
            }
            return false;
        }

        static public MeasureUnit Get(int Id)
        {
            List<MeasuresUnitFile> data = _serializer.RecoverData();
            foreach (MeasuresUnitFile br in data)
            {
                if (!br.Deleted && br.Id == Id) return ConvertToBusiness(br);
            }
            return null;
        }

        static public List<MeasureUnit> Get()
        {
            List<MeasureUnit> result = new List<MeasureUnit>();
            List<MeasuresUnitFile> data = _serializer.RecoverData();
            foreach (MeasuresUnitFile value in data)
            {
                if (!value.Deleted) result.Add(ConvertToBusiness(value));
            }
            return result;
        }

        static public void Delete(int Id)
        {
            List<MeasuresUnitFile> data = _serializer.RecoverData();
            for (int i = 0; i < data.Count; ++i)
            {
                if (data[i].Id == Id) data[i].Deleted = true;
            }
            _serializer.ClearBinary();
            _serializer.SaveData(data);
        }

        static public void Save(Business.Arrays.MeasureUnitArray values)
        {
            _serializer.ClearBinary();
            for (int i = 0; i < values.List.Size; ++i)
            {
                if (Exists(values.List[i]))
                {
                    MessageBox.Show("Value: " + values.List[i].Measure.Name + "( " + values.List[i].Unit.Name + ") alredy exists.");
                    continue;
                }

                if (values.List[i].IdMeasureUnit == -1)
                {
                    values.List[i].IdMeasureUnit = FilesConfigurations.MeasuresUnitId();
                    FilesConfigurations.PlusMeasuresUnitId();
                }

                _serializer.SaveData(ConvertToFile(values.List[i]));
            }
        }
    }
}