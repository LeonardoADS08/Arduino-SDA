using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDA_Core.Analysis;
using SDA_Core.Communication;
using System.Data;
using SDA_Core.Data.Containers;
using SDA_Core.Data.Files;
using System.Diagnostics;
using System.IO.Ports;

namespace SDA_Core
{
    public static class Program
    {
        private static Data.Containers.SensorData _receivedData;
        private static Data.Files.BaudRatesList _baudRatesList;
        private static Data.Files.SensorLists _sensorList;
        private static DataTable _serialMonitor;
        private static Data.Files.SensorLists _sensorsFormat;
        private static bool _reloadLists;

        // Para manejar archivos externos 
        private static DataSerializer<BaudRates> _baudRatesFile;
        private static DataSerializer<SensorData> _sensorListFile;

        public static SensorData ReceivedData
        {
            get  { return _receivedData; }
            set { _receivedData = value; }
        }

        public static BaudRatesList BaudRatesList
        {
            get { return _baudRatesList; }
            set { _baudRatesList = value; }
        }

        public static SensorLists SensorList
        {
            get { return _sensorList; }
            set { _sensorList = value; }
        }

        public static SensorLists SensorsFormat
        {
            get { return _sensorsFormat; }
            set { _sensorsFormat = value; }
        }

        public static bool ReloadLists
        {
            get { return _reloadLists; }
            set { _reloadLists = value; }
        }

        public static DataTable SerialMonitor
        {
            get { return _serialMonitor; }
            set { _serialMonitor = value; }
        }

        static Program()
        {
            _receivedData = new Data.Containers.SensorData();
            _baudRatesList = new Data.Files.BaudRatesList();
            _sensorList = new SensorLists();
            _sensorsFormat = new SensorLists();
            _serialMonitor = new DataTable();

            _sensorListFile = new DataSerializer<SensorData>();
            _baudRatesFile = new DataSerializer<BaudRates>();

            _baudRatesList.BaudRates = _baudRatesFile.RecoverData();
            _sensorList.SensorList = _sensorListFile.RecoverData();

        }


        static public DataTable BaudRatesToDataTable()
        {
            DataTable result = new DataTable();
            result.Columns.Add("BaudRate", typeof(double));

            foreach (BaudRates value in _baudRatesList.BaudRates)
            {               
                DataRow newRow = result.NewRow();
                newRow[0] = value.BaudRate;

                result.Rows.Add(newRow);
            }
            return result;
        }

        static public DataTable BaudRatesToDataTable(BaudRatesList data)
        {
            DataTable result = new DataTable();
            result.Columns.Add("BaudRate", typeof(double));

            foreach (BaudRates value in _baudRatesList.BaudRates)
            {
                DataRow newRow = result.NewRow();
                newRow[0] = value.BaudRate;

                result.Rows.Add(newRow);
            }
            return result;
        }

        static public DataTable SensorsToDataTable()
        {
            DataTable result = new DataTable();
            result.Columns.Add("Sensor Name", typeof(string));

            foreach (SDA_Core.Data.Containers.SensorData value in _sensorList.SensorList)
            {
                DataRow newRow = result.NewRow();
                newRow[0] = value.SensorName;

                result.Rows.Add(newRow);
            }
            return result;
        }

        static public DataTable ReceivedSensorsToDataTable()
        {
            DataTable result = new DataTable();

            result.Columns.Add("Sensor Name", typeof(string));

            foreach (SDA_Core.Data.Containers.SensorData value in _sensorsFormat.SensorList)
            {
                DataRow newRow = result.NewRow();
                newRow[0] = value.SensorName;

                result.Rows.Add(newRow);
            }

            return result;
        }

        static public List<Measurement> GetSensorsFormat()
        {
            List<Measurement> result = new List<Measurement>();
            foreach (SensorData sensor in _sensorsFormat.SensorList)
            {

                foreach(MeasurementList format in sensor.Values)
                {
                    result.Add(format.Measures[0]);
                }
            }

            return result;
        }

        static public void SaveSensorsToBinary()
        {
            _sensorListFile.ClearBinary();
            _sensorListFile.SaveData(_sensorList.SensorList);
        }

        static public void SaveBaudRatesToBinary()
        {
            _baudRatesFile.ClearBinary();
            _baudRatesFile.SaveData(_baudRatesList.BaudRates);
        }


    }
}
