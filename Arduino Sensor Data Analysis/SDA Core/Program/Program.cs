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
        private static Data.Containers.SensorData _dataStructure;
        private static DataTable _serialMonitor;
        private static Data.Files.BaudRatesList _baudRatesList;
        private static Data.Files.SensorLists _sensorList;
        private static Data.Files.SensorLists _dataStructuresList;

        public static SensorData DataStructure
        {
            get  { return _dataStructure; }
            set { _dataStructure = value; }
        }

        public static DataTable SerialMonitor
        {
            get { return _serialMonitor; }
            set { _serialMonitor = value; }
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

        public static SensorLists DataStructuresList
        {
            get { return _dataStructuresList; }
            set { _dataStructuresList = value; }
        }

        static Program()
        {
            _dataStructure = new Data.Containers.SensorData();
            _serialMonitor = new DataTable();
            _baudRatesList = new Data.Files.BaudRatesList();
            _sensorList = new SensorLists();
            _dataStructuresList = new SensorLists();
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
    }

}
