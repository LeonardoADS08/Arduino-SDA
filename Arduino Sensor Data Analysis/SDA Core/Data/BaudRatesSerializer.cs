using SDA_Core.Business;
using SDA_Core.Functional;
using System.Collections.Generic;
using System.Windows;

namespace SDA_Core.Data
{
    public static class BaudRatesSerializer
    {
        static private Functional.DataSerializer<BaudRateFile> _serializer;

        static BaudRatesSerializer()
        {
            _serializer = new DataSerializer<BaudRateFile>();
        }

        static private BaudRates ConvertToBusiness(BaudRateFile value)
        {
            BaudRates result = new BaudRates();
            result.IdBaudRate = value.Id;
            result.Value = value.BaudRate;

            return result;
        }

        static private BaudRateFile ConvertToFile(BaudRates value)
        {
            return new BaudRateFile(value);
        }

        static public bool Exists(BaudRates value)
        {
            List<BaudRateFile> data = _serializer.RecoverData();
            foreach (BaudRateFile br in data)
            {
                if (!br.Deleted && br.BaudRate == value.Value) return true;
            }
            return false;
        }

        static public BaudRates Get(int Id)
        {
            List<BaudRateFile> data = _serializer.RecoverData();
            foreach (BaudRateFile br in data)
            {
                if (!br.Deleted && br.Id == Id) return ConvertToBusiness(br);
            }
            return null;
        }

        static public List<BaudRates> Get()
        {
            List<BaudRates> result = new List<BaudRates>();
            List<BaudRateFile> data = _serializer.RecoverData();
            foreach (BaudRateFile value in data)
            {
                if (!value.Deleted) result.Add(ConvertToBusiness(value));
            }
            return result;
        }

        static public void Delete(int Id)
        {
            List<BaudRateFile> data = _serializer.RecoverData();
            for (int i = 0; i < data.Count; ++i)
            {
                if (data[i].Id == Id) data[i].Deleted = true;
            }
            _serializer.ClearBinary();
            _serializer.SaveData(data);
        }

        static public void Save(Business.Arrays.BaudRatesArray values)
        {
            _serializer.ClearBinary();
            for (int i = 0; i < values.List.Size; ++i)
            {
                if (Exists(values.List[i]))
                {
                    MessageBox.Show("Value: " + values.List[i].Value + " alredy exists.");
                    continue;
                }

                if (values.List[i].IdBaudRate == -1)
                {
                    values.List[i].IdBaudRate = FilesConfigurations.BaudRatesId();
                    FilesConfigurations.PlusBaudRatesId();
                }

                _serializer.SaveData(ConvertToFile(values.List[i]));
            }
        }
    }
}