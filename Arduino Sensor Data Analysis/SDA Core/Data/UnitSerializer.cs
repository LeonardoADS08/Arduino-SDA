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
    public static class UnitSerializer 
    {
        static private Functional.DataSerializer<UnitFile> _serializer;

        static UnitSerializer()
        {
            _serializer = new DataSerializer<UnitFile>();
        }

        static private Unit ConvertToBusiness(UnitFile value)
        {
            Unit result = new Unit();
            result.IdUnit = value.Id;
            result.Name = value.Unit;

            return result;
        }

        static private UnitFile ConvertToFile(Unit value)
        {
            return new UnitFile(value);
        }

        static public bool Exists(Unit value)
        {
            List<UnitFile> data = _serializer.RecoverData();
            foreach (UnitFile val in data)
            {
                if (!val.Deleted && val.Unit == value.Name) return true;
            }
            return false;
        }

        static public Unit Get(int Id)
        {
            List<UnitFile> data = _serializer.RecoverData();
            foreach (UnitFile value in data)
            {
                if (!value.Deleted && value.Id == Id) return ConvertToBusiness(value);
            }
            return null;
        }

        static public List<Unit> Get()
        {
            List<Unit> result = new List<Unit>();
            List<UnitFile> data = _serializer.RecoverData();
            foreach (UnitFile value in data)
            {
                if (!value.Deleted) result.Add(ConvertToBusiness(value));
            }
            return result;
        }

        static public void Delete(int Id)
        {
            List<UnitFile> data = _serializer.RecoverData();
            for (int i = 0; i < data.Count; ++i)
            {
                if (data[i].Id == Id) data[i].Deleted = true;
            }
            _serializer.ClearBinary();
            _serializer.SaveData(data);
        }

        static public void Save(Business.Arrays.UnitArray values)
        {
            _serializer.ClearBinary();
            for (int i = 0; i < values.List.Size; ++i)
            {
                if (Exists(values.List[i]))
                {
                    MessageBox.Show("Value: " + values.List[i].Name + " alredy exists.");
                    continue;
                }

                if (values.List[i].IdUnit == -1)
                {
                    values.List[i].IdUnit = FilesConfigurations.UnitsId();
                    FilesConfigurations.PlusUnitsId();
                }

                _serializer.SaveData(ConvertToFile(values.List[i]));
            }
        }

    }
}
