using SDA_Core.Business;
using SDA_Core.Business.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA_Program
{
    static public class Data
    {
        static private List<BaudRates> _baudRatesData;
        static private List<Measure> _measuresData;
        static private List<MeasureUnit> _measureUnitsData;
        static private List<SensorData> _sensorsData;
        static private List<Unit> _unitsData;

        static Data()
        {
            _baudRatesData = SDA_Core.Data.BaudRatesSerializer.Get();
            _measuresData = SDA_Core.Data.MeasuresSerializer.Get();
            _measureUnitsData = SDA_Core.Data.MeasureUnitSerializer.Get();
            _sensorsData = SDA_Core.Data.SensorDataSerializer.Get();
            _unitsData = SDA_Core.Data.UnitSerializer.Get();
        }

        public static List<BaudRates> BaudRatesDataList { get => _baudRatesData; set => _baudRatesData = value; }
        public static List<Measure> MeasuresDataList { get => _measuresData; set => _measuresData = value; }
        public static List<MeasureUnit> MeasureUnitsDataList { get => _measureUnitsData; set => _measureUnitsData = value; }
        public static List<SensorData> SensorsDataList { get => _sensorsData; set => _sensorsData = value; }
        public static List<Unit> UnitsDataList { get => _unitsData; set => _unitsData = value; }

        public static BaudRatesArray BaudRatesDataArray
        {
            get
            {
                BaudRatesArray result = new BaudRatesArray();
                result.List = new GenericArray<BaudRates>(_baudRatesData);
                return result;
            }
            set
            {
                _baudRatesData = value.List.GetList();
            }
        }

        public static MeasureArray MeasuresDataArray
        {
            get
            {
                MeasureArray result = new MeasureArray();
                result.List = new GenericArray<Measure>(_measuresData);
                return result;
            }
            set
            {
                _measuresData = value.List.GetList();
            }
        }

        public static MeasureUnitArray MeasureUnitsDataArray
        {
            get
            {
                MeasureUnitArray result = new MeasureUnitArray();
                result.List = new GenericArray<MeasureUnit>(_measureUnitsData);
                return result;
            }
            set
            {
                _measureUnitsData = value.List.GetList();
            }
        }

        public static SensorDataArray SensorsDataArray
        {
            get
            {
                SensorDataArray result = new SensorDataArray();
                result.List = new GenericArray<SensorData>(_sensorsData);
                return result;
            }
            set
            {
                _sensorsData = value.List.GetList();
            }
        }

        public static UnitArray UnitsDataArray
        {
            get
            {
                UnitArray result = new UnitArray();
                result.List = new GenericArray<Unit>(_unitsData);
                return result;
            }
            set
            {
                _unitsData = value.List.GetList();
            }
        }
    }
}